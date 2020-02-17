using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Curds.Persistence.Model.Implementation
{
    internal abstract class BaseExpressionBuilder
    {
        protected Expression CallMethodExpression(ParameterExpression calledObject, MethodInfo methodToCall, Expression valueExpression) =>
            Expression.Call(calledObject, methodToCall, valueExpression);
        protected Expression CallMethodExpression<TCastType>(ParameterExpression calledObject, MethodInfo methodToCall, Expression valueExpression) =>
            Expression.Call(calledObject, methodToCall, Expression.Convert(valueExpression, typeof(TCastType)));
    }
}
