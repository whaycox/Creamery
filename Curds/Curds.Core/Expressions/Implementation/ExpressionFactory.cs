using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Implementation
{
    using Abstraction;

    internal class ExpressionFactory : IExpressionFactory
    {
        public ParameterExpression Parameter<TEntity>(string name) => Expression.Parameter(typeof(TEntity), name);
        public ParameterExpression Variable<TEntity>(string name) => Expression.Variable(typeof(TEntity), name);

        public Expression Block(params Expression[] blockExpressions) => Expression.Block(blockExpressions);
        public Expression Block(IEnumerable<ParameterExpression> parameterExpressions, params Expression[] blockExpressions) => Expression.Block(parameterExpressions, blockExpressions);
        public Expression Block(IEnumerable<ParameterExpression> parameterExpressions, IEnumerable<Expression> blockExpressions) => Expression.Block(parameterExpressions, blockExpressions);
        public Expression New(ConstructorInfo constructor, params Expression[] constructorArguments) => Expression.New(constructor, constructorArguments);
        public Expression Assign(Expression instance, Expression value) => Expression.Assign(instance, value);
        public Expression Convert<TEntity>(Expression instance) => Expression.Convert(instance, typeof(TEntity));
        public Expression Call(Expression instance, MethodInfo method, params Expression[] methodArguments) => Expression.Call(instance, method, methodArguments);
        public Expression Loop(Expression loopBody, LabelTarget breakLabel) => Expression.Loop(loopBody, breakLabel);
        public Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters) => Expression.Lambda<TDelegate>(body, parameters);

        public Expression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse) => Expression.IfThenElse(test, ifTrue, ifFalse);
        public Expression LessThan(Expression left, Expression right) => Expression.LessThan(left, right);

        public Expression PostIncrementAssign(Expression expression) => Expression.PostIncrementAssign(expression);

        public LabelTarget Label(string name) => Expression.Label(name);
        public LabelTarget Label(Type type) => Expression.Label(type);

        public Expression Label(LabelTarget returnLabel, Expression defaultValue) => Expression.Label(returnLabel, defaultValue);
        public Expression Return(LabelTarget returnLabel, Expression value) => Expression.Return(returnLabel, value);

        public Expression Break(LabelTarget label) => Expression.Break(label);
    }
}
