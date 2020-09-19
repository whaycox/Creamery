using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    internal abstract class BaseExpressionBuilder
    {
        protected Expression CallMethodExpression(ParameterExpression calledObject, MethodInfo methodToCall, Expression valueExpression) =>
            Expression.Call(calledObject, methodToCall, valueExpression);
        protected Expression CallMethodExpression<TCastType>(ParameterExpression calledObject, MethodInfo methodToCall, Expression valueExpression) =>
            CallMethodExpressionAndCast(calledObject, methodToCall, valueExpression, typeof(TCastType));
        protected Expression CallMethodExpressionAndCast(ParameterExpression calledObject, MethodInfo methodToCall, Expression valueExpression, Type castType) =>
            Expression.Call(calledObject, methodToCall, Expression.Convert(valueExpression, castType));
    }
}
