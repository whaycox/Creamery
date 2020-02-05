using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;

    internal class ExpressionParser : IExpressionParser
    {
        public string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression) 
            where TEntity : BaseEntity
        {
            if (valueSelectionExpression.NodeType != ExpressionType.Lambda ||
                valueSelectionExpression.Body.NodeType != ExpressionType.MemberAccess)
                throw InvalidValueSelectionExpression(valueSelectionExpression);

            MemberExpression memberAccessExpression = (MemberExpression)valueSelectionExpression.Body;
            if (memberAccessExpression.Expression.NodeType != ExpressionType.Parameter)
                throw InvalidValueSelectionExpression(valueSelectionExpression);
            return memberAccessExpression.Member.Name;
        }
        private FormatException InvalidValueSelectionExpression<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : BaseEntity => new FormatException($"Invalid entity value selection: {valueSelectionExpression}");
    }
}
