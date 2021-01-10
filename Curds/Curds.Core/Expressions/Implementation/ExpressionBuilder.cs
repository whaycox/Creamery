using System;
using System.Collections;
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
        private PropertyInfo CollectionCountProperty { get; } = typeof(ICollection).GetProperty(nameof(ICollection.Count));

        public ParameterExpression AddParameter<TEntity>(string name)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), name);
            ParameterExpressions.Add(parameterExpression);

            return parameterExpression;
        }

        private ParameterExpression AddVariable<TEntity>(string name) => AddVariable(typeof(TEntity), name);
        private ParameterExpression AddVariable(Type variableType, string name)
        {
            ParameterExpression variableExpression = Expression.Parameter(variableType, name);
            BodyVariables.Add(variableExpression);

            return variableExpression;
        }

        public ParameterExpression CreateObject<TEntity>(string name) => CreateObject<TEntity>(name, new Type[0], null);
        public ParameterExpression CreateObject<TEntity>(string name, Type[] constructorTypes, Expression[] constructorValues)
        {
            ConstructorInfo constructor = typeof(TEntity).GetConstructor(constructorTypes);
            ParameterExpression variableExpression = AddVariable<TEntity>(name);
            Expression createObjectExpression = Expression.Assign(variableExpression, Expression.New(constructor, constructorValues));

            BodyExpressions.Add(createObjectExpression);
            return variableExpression;
        }

        public Expression ConvertExpressionType<TTarget>(Expression source) => Expression.Convert(source, typeof(TTarget));

        public Expression CallMethod(Expression variable, MethodInfo method, params Expression[] arguments) =>
            Expression.Call(variable, method, arguments);
        public Expression GetProperty(Expression variable, PropertyInfo property) =>
            Expression.Call(variable, property.GetMethod);

        public void SetProperty(ParameterExpression variable, PropertyInfo property, Expression value) =>
            BodyExpressions.Add(Expression.Call(variable, property.SetMethod, value));

        public void For(Expression collectionExpression, Func<ParameterExpression, Expression> contentExpressionDelegate)
        {
            ParameterExpression iteratorVariable = Expression.Parameter(typeof(int), nameof(iteratorVariable));
            LabelTarget breakLabel = Expression.Label(nameof(breakLabel));

            Expression forBlock = Expression.Block(
                new[] { iteratorVariable },
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(iteratorVariable, GetProperty(collectionExpression, CollectionCountProperty)),
                        Expression.Block(
                            contentExpressionDelegate(iteratorVariable),
                            Expression.PostIncrementAssign(iteratorVariable)),
                        Expression.Break(breakLabel)),
                    breakLabel)
                );

            BodyExpressions.Add(forBlock);
        }

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
