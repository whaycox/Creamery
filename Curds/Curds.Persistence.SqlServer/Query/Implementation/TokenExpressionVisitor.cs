using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Expressions.Nodes.Domain;
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

        public override void VisitConstant(ConstantNode constantNode)
        {
            object value = constantNode.SourceExpression.Value;

            if (VisitationState.Count > 0 && VisitationState.Peek() is FieldInfo)
            {
                FieldInfo fieldInfo = VisitationState.Pop() as FieldInfo;

                throw new System.NotImplementedException();
                //return TokenFactory.Parameter(Context.ParameterBuilder, fieldInfo.Name, fieldInfo.GetValue(value));
            }
            else
            {

                throw new System.NotImplementedException();
                //return TokenFactory.Parameter(Context.ParameterBuilder, nameof(value), value);
            }
        }

        public override void VisitConvert(ConvertNode convertNode) =>
            convertNode.Operand.AcceptVisitor(this);

        public override void VisitEqual(EqualNode equalNode)
        {

            throw new System.NotImplementedException();
            //ISqlQueryToken left = equalNode.Left.AcceptVisitor(this);
            //ISqlQueryToken right = equalNode.Right.AcceptVisitor(this);

            //if (IsKeyEquality(left, right, out ISqlQueryToken property))
            //    return ExpandKeyEquality(property, property == left ? right : left);
            //else
            //    return TokenFactory.BooleanComparison(BooleanComparison.Equals, left, right);
        }
        private bool IsKeyEquality(ISqlQueryToken left, ISqlQueryToken right, out ISqlQueryToken property)
        {
            property = null;

            if (left is ColumnNameSqlQueryToken)
            {
                ColumnNameSqlQueryToken columnName = (ColumnNameSqlQueryToken)left;
                if (columnName.Column.ValueName == nameof(IEntity.Keys))
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
            ColumnNameSqlQueryToken columnName = (ColumnNameSqlQueryToken)property;
            ISqlTable table = columnName.Column.Table;
            IList<ISqlColumn> keys = table.Keys;

            ParameterSqlQueryToken parameterSqlQueryToken = (ParameterSqlQueryToken)value;
            object[] keyValues = (object[])Context.ParameterBuilder.UnregisterParameter(parameterSqlQueryToken.Name);

            if (keys.Count != keyValues.Length)
                throw new ArgumentException($"Invalid number of keys supplied, expected {keys.Count} but {keyValues.Length} were provided");

            List<ISqlQueryToken> keyTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < keys.Count; i++)
            {
                ISqlQueryToken keyQualifiedObject = TokenFactory.ColumnName(keys[i]);
                ISqlQueryToken keyValue = TokenFactory.Parameter(Context.ParameterBuilder, keys[i].Name, keyValues[i]);

                keyTokens.Add(TokenFactory.BooleanComparison(BooleanComparison.Equals, keyQualifiedObject, keyValue));
            }

            return TokenFactory.BooleanCombination(BooleanCombination.And, keyTokens);
        }

        public override void VisitNotEqual(NotEqualNode notEqualNode)
        {

            throw new System.NotImplementedException();
            //ISqlQueryToken left = notEqualNode.Left.AcceptVisitor(this);
            //ISqlQueryToken right = notEqualNode.Right.AcceptVisitor(this);
            //return TokenFactory.BooleanComparison(BooleanComparison.NotEquals, left, right);
        }

        public override void VisitLessThan(LessThanNode lessThanNode)
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken left = lessThanNode.Left.AcceptVisitor(this);
            //ISqlQueryToken right = lessThanNode.Right.AcceptVisitor(this);
            //return TokenFactory.BooleanComparison(BooleanComparison.LessThan, left, right);
        }

        public override void VisitLessThanOrEqual(LessThanOrEqualNode lessThanOrEqualNode)
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken left = lessThanOrEqualNode.Left.AcceptVisitor(this);
            //ISqlQueryToken right = lessThanOrEqualNode.Right.AcceptVisitor(this);
            //return TokenFactory.BooleanComparison(BooleanComparison.LessThanOrEquals, left, right);
        }

        public override void VisitLambda(LambdaNode lambdaNode) =>
            lambdaNode.Body.AcceptVisitor(this);

        public override void VisitMemberAccess(MemberAccessNode memberAccessNode)
        {
            throw new System.NotImplementedException();
            //VisitationState.Push(memberAccessNode.SourceExpression.Member);
            //return memberAccessNode.Expression.AcceptVisitor(this);
        }

        public override void VisitParameter(ParameterNode parameterNode)
        {
            throw new System.NotImplementedException();
            //ISqlTable table = Context.ParseTableExpression(parameterNode.SourceExpression);
            //PropertyInfo propertyInfo = VisitationState.Pop() as PropertyInfo;

            //ISqlColumn column = null;
            //if (propertyInfo.Name == nameof(IEntity.Keys))
            //    column = table.KeyColumn;
            //else
            //    column = table.Columns.First(col => col.ValueName == propertyInfo.Name);
            //return TokenFactory.ColumnName(column);
        }

        public override void VisitModulo(ModuloNode moduloNode)
        {

            throw new System.NotImplementedException();
            //ISqlQueryToken left = moduloNode.Left.AcceptVisitor(this);
            //ISqlQueryToken right = moduloNode.Right.AcceptVisitor(this);
            //return TokenFactory.ArithmeticOperation(ArithmeticOperation.Modulo, left, right);
        }

        public override ISqlQueryToken Build()
        {
            throw new NotImplementedException();
        }
    }
}
