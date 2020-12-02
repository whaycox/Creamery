using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Implementation
{
    using Abstraction;

    internal class ExpressionBuilder : IExpressionBuilder
    {
        private List<ParameterExpression> ParameterExpressions { get; } = new List<ParameterExpression>();
        private List<ParameterExpression> BodyVariables { get; } = new List<ParameterExpression>();
        private List<Expression> BodyExpressions { get; } = new List<Expression>();

        public ParameterExpression AddParameter<TEntity>(string name)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), name);
            ParameterExpressions.Add(parameterExpression);

            return parameterExpression;
        }

        public ParameterExpression CreateObject<TEntity>(string name)
        {
            ConstructorInfo constructor = typeof(TEntity).GetConstructor(new Type[0]);
            ParameterExpression variableExpression = Expression.Parameter(typeof(TEntity), name);
            Expression createObjectExpression = Expression.Assign(variableExpression, Expression.New(constructor));

            BodyVariables.Add(variableExpression);
            BodyExpressions.Add(createObjectExpression);
            return variableExpression;
        }

        public Expression CallMethod(ParameterExpression variable, MethodInfo method, params Expression[] arguments) =>
            Expression.Call(variable, method, arguments);
        public Expression GetProperty(ParameterExpression variable, PropertyInfo property) =>
            Expression.Call(variable, property.GetMethod);

        public void SetProperty(ParameterExpression variable, PropertyInfo property, Expression value) =>
            BodyExpressions.Add(Expression.Call(variable, property.SetMethod, value));

        public void ReturnObject(ParameterExpression variable)
        {
            LabelTarget returnLabel = Expression.Label(variable.Type);
            BodyExpressions.Add(Expression.Return(returnLabel, variable));
            BodyExpressions.Add(Expression.Label(returnLabel, variable));
        }

        public TDelegate Build<TDelegate>()
        {
            Expression body = Expression.Block(BodyVariables, BodyExpressions);
            return Expression
                .Lambda<TDelegate>(body, ParameterExpressions)
                .Compile();
        }
    }
}
