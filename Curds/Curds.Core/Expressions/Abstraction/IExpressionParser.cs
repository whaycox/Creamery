using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Abstraction
{
    internal interface IExpressionParser
    {
        PropertyInfo ParsePropertyExpression<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertyExpression);
    }
}
