using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Abstraction
{
    public interface IExpressionParser
    {
        PropertyInfo ParsePropertyExpression<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertyExpression);
    }
}
