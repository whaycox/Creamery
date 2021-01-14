using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Abstraction
{
    public interface IExpressionFactory
    {
        ParameterExpression Parameter<TEntity>(string name);
        ParameterExpression Variable<TEntity>(string name);

        Expression Block(params Expression[] blockExpressions);
        Expression Block(IEnumerable<ParameterExpression> parameterExpressions, params Expression[] blockExpressions);
        Expression Block(IEnumerable<ParameterExpression> parameterExpressions, IEnumerable<Expression> blockExpressions);
        Expression New(ConstructorInfo constructor, params Expression[] constructorArguments);
        Expression Assign(Expression instance, Expression value);
        Expression Convert<TEntity>(Expression instance);
        Expression Call(Expression instance, MethodInfo method, params Expression[] methodArguments);
        Expression Loop(Expression loopBody, LabelTarget breakLabel);
        Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters);

        Expression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse);
        Expression LessThan(Expression left, Expression right);

        Expression PostIncrementAssign(Expression expression);

        LabelTarget Label(string name);
        LabelTarget Label(Type type);

        Expression Label(LabelTarget returnLabel, Expression defaultValue);
        Expression Return(LabelTarget returnLabel, Expression value);

        Expression Break(LabelTarget label);
    }
}
