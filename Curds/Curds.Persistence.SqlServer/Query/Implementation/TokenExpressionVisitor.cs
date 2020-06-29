using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using ExpressionNodes.Domain;
    using Persistence.Abstraction;
    using Tokens.Implementation;

    internal class TokenExpressionVisitor<TModel> : BaseQueryExpressionVisitor<TModel, ISqlQueryToken>, ISqlQueryTokenVisitor<TModel>
        where TModel : IDataModel
    {
        public ISqlQueryTokenFactory TokenFactory { get; }

        private Stack<object> VisitationState { get; } = new Stack<object>();

        public TokenExpressionVisitor(
            ISqlQueryTokenFactory tokenFactory,
            ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        {
            TokenFactory = tokenFactory;
        }

        public override ISqlQueryToken VisitConstant(ConstantNode constantNode)
        {
            object value = constantNode.SourceExpression.Value;

            if (VisitationState.Count > 0 && VisitationState.Peek() is FieldInfo)
            {
                FieldInfo fieldInfo = VisitationState.Pop() as FieldInfo;
                return TokenFactory.Parameter(Context.ParameterBuilder, fieldInfo.Name, fieldInfo.GetValue(value));
            }
            else
            {
                return TokenFactory.Parameter(Context.ParameterBuilder, nameof(value), value);
            }
        }

        public override ISqlQueryToken VisitConvert(ConvertNode convertNode) =>
            convertNode.Operand.AcceptVisitor(this);

        public override ISqlQueryToken VisitEqual(EqualNode equalNode)
        {
            ISqlQueryToken left = equalNode.Left.AcceptVisitor(this);
            ISqlQueryToken right = equalNode.Right.AcceptVisitor(this);

            if (IsKeyEquality(left, right, out ISqlQueryToken property))
                return ExpandKeyEquality(property, property == left ? right : left);
            else
                return TokenFactory.BooleanComparison(BooleanComparison.Equals, left, right);
        }
        private bool IsKeyEquality(ISqlQueryToken left, ISqlQueryToken right, out ISqlQueryToken property)
        {
            property = null;

            if (left is QualifiedObjectSqlQueryToken)
            {
                QualifiedObjectSqlQueryToken qualifiedObject = (QualifiedObjectSqlQueryToken)left;
                if (qualifiedObject.Column != null && qualifiedObject.Column.ValueName == nameof(IEntity.Keys))
                {
                    if (right is ParameterSqlQueryToken)
                    {
                        ParameterSqlQueryToken parameterSqlQueryToken = (ParameterSqlQueryToken)right;
                        if (parameterSqlQueryToken.Type == typeof(object[]))
                        {
                            property = left;
                            return true;
                        }
                    }
                }
            }
            else if (right is QualifiedObjectSqlQueryToken)
            {
                throw new NotImplementedException();
            }

            return false;
        }
        private ISqlQueryToken ExpandKeyEquality(ISqlQueryToken property, ISqlQueryToken value)
        {
            QualifiedObjectSqlQueryToken qualifiedObject = (QualifiedObjectSqlQueryToken)property;
            ISqlTable table = qualifiedObject.Column.Table;
            IList<ISqlColumn> keys = table.Keys;

            ParameterSqlQueryToken parameterSqlQueryToken = (ParameterSqlQueryToken)value;
            object[] keyValues = (object[])Context.ParameterBuilder.UnregisterParameter(parameterSqlQueryToken.Name);

            if (keys.Count != keyValues.Length)
                throw new ArgumentException($"Invalid number of keys supplied, expected {keys.Count} but {keyValues.Length} were provided");

            List<ISqlQueryToken> keyTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < keys.Count; i++)
            {
                ISqlQueryToken keyQualifiedObject = TokenFactory.QualifiedObjectName(keys[i]);
                ISqlQueryToken keyValue = TokenFactory.Parameter(Context.ParameterBuilder, keys[i].Name, keyValues[i]);

                keyTokens.Add(TokenFactory.BooleanComparison(BooleanComparison.Equals, keyQualifiedObject, keyValue));
            }

            return TokenFactory.BooleanCombination(BooleanCombination.And, keyTokens);
        }

        public override ISqlQueryToken VisitLessThan(LessThanNode lessThanNode)
        {
            ISqlQueryToken left = lessThanNode.Left.AcceptVisitor(this);
            ISqlQueryToken right = lessThanNode.Right.AcceptVisitor(this);
            return TokenFactory.BooleanComparison(BooleanComparison.LessThan, left, right);
        }

        public override ISqlQueryToken VisitLessThanOrEqual(LessThanOrEqualNode lessThanOrEqualNode)
        {
            ISqlQueryToken left = lessThanOrEqualNode.Left.AcceptVisitor(this);
            ISqlQueryToken right = lessThanOrEqualNode.Right.AcceptVisitor(this);
            return TokenFactory.BooleanComparison(BooleanComparison.LessThanOrEqual, left, right);
        }

        public override ISqlQueryToken VisitLambda(LambdaNode lambdaNode) =>
            lambdaNode.Body.AcceptVisitor(this);

        public override ISqlQueryToken VisitMemberAccess(MemberAccessNode memberAccessNode)
        {
            VisitationState.Push(memberAccessNode.SourceExpression.Member);
            return memberAccessNode.Expression.AcceptVisitor(this);
        }

        public override ISqlQueryToken VisitParameter(ParameterNode parameterNode)
        {
            ISqlTable table = Context.ParseTableExpression(parameterNode.SourceExpression);
            PropertyInfo propertyInfo = VisitationState.Pop() as PropertyInfo;

            ISqlColumn column = null;
            if (propertyInfo.Name == nameof(IEntity.Keys))
                column = table.KeyColumn;
            else
                column = table.Columns.First(col => col.ValueName == propertyInfo.Name);
            return TokenFactory.QualifiedObjectName(column);
        }

        public override ISqlQueryToken VisitModulo(ModuloNode moduloNode)
        {
            ISqlQueryToken left = moduloNode.Left.AcceptVisitor(this);
            ISqlQueryToken right = moduloNode.Right.AcceptVisitor(this);
            return TokenFactory.ArithmeticOperation(ArithmeticOperation.Modulo, left, right);
        }
    }
}
