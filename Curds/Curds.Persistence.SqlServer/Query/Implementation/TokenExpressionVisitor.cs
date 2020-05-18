using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Domain;
    using Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using ExpressionNodes.Implementation;
    using Persistence.Implementation;
    using Tokens.Implementation;
    using Persistence.Domain;

    internal class TokenExpressionVisitor<TModel> : BaseExpressionVisitor<ISqlQueryToken, ISqlQueryContext<TModel>>, ISqlQueryTokenVisitor<TModel>
        where TModel : IDataModel
    {
        public override ISqlQueryToken VisitConstant(ISqlQueryContext<TModel> context, ConstantNode<ISqlQueryToken, ISqlQueryContext<TModel>> constantNode)
        {
            object value = constantNode.SourceExpression.Value;

            if (VisitationState.Count > 0 && VisitationState.Peek() is FieldInfo)
            {
                FieldInfo fieldInfo = VisitationState.Pop() as FieldInfo;
                return context.TokenFactory.Parameter(context.ParameterBuilder, fieldInfo.Name, fieldInfo.GetValue(value));
            }
            else
            {
                return context.TokenFactory.Parameter(context.ParameterBuilder, nameof(value), value);
            }
        }

        public override ISqlQueryToken VisitConvert(ISqlQueryContext<TModel> context, ConvertNode<ISqlQueryToken, ISqlQueryContext<TModel>> convertNode) =>
            convertNode.Operand.AcceptVisitor(this, context);

        public override ISqlQueryToken VisitEqual(ISqlQueryContext<TModel> context, EqualNode<ISqlQueryToken, ISqlQueryContext<TModel>> equalNode)
        {
            ISqlQueryToken left = equalNode.Left.AcceptVisitor(this, context);
            ISqlQueryToken right = equalNode.Right.AcceptVisitor(this, context);
            if (IsKeyEquality(left, right, out ISqlQueryToken property))
                return ExpandKeyEquality(context, property, property == left ? right : left);
            else
                return context.TokenFactory.BooleanComparison(BooleanComparison.Equals, left, right);
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
        private ISqlQueryToken ExpandKeyEquality(ISqlQueryContext<TModel> context, ISqlQueryToken property, ISqlQueryToken value)
        {
            QualifiedObjectSqlQueryToken qualifiedObject = (QualifiedObjectSqlQueryToken)property;
            ISqlTable table = qualifiedObject.Column.Table;
            IList<ISqlColumn> keys = table.Keys;

            ParameterSqlQueryToken parameterSqlQueryToken = (ParameterSqlQueryToken)value;
            object[] keyValues = (object[])context.ParameterBuilder.UnregisterParameter(parameterSqlQueryToken.Name);

            if (keys.Count != keyValues.Length)
                throw new ArgumentException($"Invalid number of keys supplied, expected {keys.Count} but {keyValues.Length} were provided");

            List<ISqlQueryToken> keyTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < keys.Count; i++)
            {
                ISqlQueryToken keyQualifiedObject = context.TokenFactory.QualifiedObjectName(keys[i]);
                ISqlQueryToken keyValue = context.TokenFactory.Parameter(context.ParameterBuilder, keys[i].Name, keyValues[i]);

                keyTokens.Add(context.TokenFactory.BooleanComparison(BooleanComparison.Equals, keyQualifiedObject, keyValue));
            }

            return context.TokenFactory.BooleanCombination(BooleanCombination.And, keyTokens);
        }

        public override ISqlQueryToken VisitLessThan(ISqlQueryContext<TModel> context, LessThanNode<ISqlQueryToken, ISqlQueryContext<TModel>> lessThanNode)
        {
            ISqlQueryToken left = lessThanNode.Left.AcceptVisitor(this, context);
            ISqlQueryToken right = lessThanNode.Right.AcceptVisitor(this, context);
            return context.TokenFactory.BooleanComparison(BooleanComparison.LessThan, left, right);
        }

        public override ISqlQueryToken VisitLessThanOrEqual(ISqlQueryContext<TModel> context, LessThanOrEqualNode<ISqlQueryToken, ISqlQueryContext<TModel>> lessThanOrEqualNode)
        {
            ISqlQueryToken left = lessThanOrEqualNode.Left.AcceptVisitor(this, context);
            ISqlQueryToken right = lessThanOrEqualNode.Right.AcceptVisitor(this, context);
            return context.TokenFactory.BooleanComparison(BooleanComparison.LessThanOrEqual, left, right);
        }

        public override ISqlQueryToken VisitLambda(ISqlQueryContext<TModel> context, LambdaNode<ISqlQueryToken, ISqlQueryContext<TModel>> lambdaNode) =>
            lambdaNode.Body.AcceptVisitor(this, context);

        public override ISqlQueryToken VisitMemberAccess(ISqlQueryContext<TModel> context, MemberAccessNode<ISqlQueryToken, ISqlQueryContext<TModel>> memberAccessNode)
        {
            VisitationState.Push(memberAccessNode.SourceExpression.Member);
            return memberAccessNode.Expression.AcceptVisitor(this, context);
        }

        public override ISqlQueryToken VisitParameter(ISqlQueryContext<TModel> context, ParameterNode<ISqlQueryToken, ISqlQueryContext<TModel>> parameterNode)
        {
            ISqlTable table = context.ParseTableExpression(parameterNode.SourceExpression);
            PropertyInfo propertyInfo = VisitationState.Pop() as PropertyInfo;

            ISqlColumn column = null;
            if (propertyInfo.Name == nameof(IEntity.Keys))
                column = table.KeyColumn;
            else
                column = table.Columns.First(col => col.ValueName == propertyInfo.Name);
            return context.TokenFactory.QualifiedObjectName(column);
        }

        public override ISqlQueryToken VisitModulo(ISqlQueryContext<TModel> context, ModuloNode<ISqlQueryToken, ISqlQueryContext<TModel>> moduloNode)
        {
            ISqlQueryToken left = moduloNode.Left.AcceptVisitor(this, context);
            ISqlQueryToken right = moduloNode.Right.AcceptVisitor(this, context);
            return context.TokenFactory.ArithmeticOperation(ArithmeticOperation.Modulo, left, right);
        }
    }
}
