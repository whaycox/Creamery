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
        private IExpressionFactory ExpressionFactory { get; }

        public List<ParameterExpression> ParameterExpressions { get; } = new List<ParameterExpression>();
        public List<ParameterExpression> BodyVariables { get; } = new List<ParameterExpression>();
        public List<Expression> BodyExpressions { get; } = new List<Expression>();
        public PropertyInfo CollectionCountProperty { get; } = typeof(ICollection).GetProperty(nameof(ICollection.Count));

        public ExpressionBuilder(IExpressionFactory expressionFactory)
        {
            ExpressionFactory = expressionFactory;
        }

        public ParameterExpression AddParameter<TEntity>(string name)
        {
            ParameterExpression parameterExpression = ExpressionFactory.Parameter<TEntity>(name);
            ParameterExpressions.Add(parameterExpression);

            return parameterExpression;
        }

        public ParameterExpression CreateObject<TEntity>(string name) => CreateObject<TEntity>(name, new Type[0], null);
        public ParameterExpression CreateObject<TEntity>(string name, Type[] constructorTypes, Expression[] constructorValues)
        {
            ConstructorInfo constructor = typeof(TEntity).GetConstructor(constructorTypes);
            ParameterExpression variableExpression = AddVariable<TEntity>(name);
            Expression createObjectExpression = ExpressionFactory.Assign(
                variableExpression,
                ExpressionFactory.New(
                    constructor,
                    constructorValues));

            BodyExpressions.Add(createObjectExpression);
            return variableExpression;
        }
        private ParameterExpression AddVariable<TEntity>(string name)
        {
            ParameterExpression variableExpression = ExpressionFactory.Variable<TEntity>(name);
            BodyVariables.Add(variableExpression);

            return variableExpression;
        }

        public void SetProperty(ParameterExpression variable, PropertyInfo property, Expression value) =>
            BodyExpressions.Add(ExpressionFactory.Call(variable, property.SetMethod, value));

        public void For(Expression collectionExpression, Func<ParameterExpression, Expression> contentExpressionDelegate)
        {
            ParameterExpression iteratorVariable = ExpressionFactory.Variable<int>(nameof(iteratorVariable));
            LabelTarget breakLabel = ExpressionFactory.Label(nameof(breakLabel));

            Expression forBlock = ExpressionFactory.Block(
                new[] { iteratorVariable },
                ExpressionFactory.Loop(
                    ExpressionFactory.IfThenElse(
                        ExpressionFactory.LessThan(
                            iteratorVariable,
                            ExpressionFactory.Call(
                                collectionExpression,
                                CollectionCountProperty.GetMethod)),
                        ExpressionFactory.Block(
                            contentExpressionDelegate(iteratorVariable),
                            ExpressionFactory.PostIncrementAssign(iteratorVariable)),
                        ExpressionFactory.Break(breakLabel)),
                    breakLabel)
                );

            BodyExpressions.Add(forBlock);
        }

        public void ReturnObject(ParameterExpression variable)
        {
            LabelTarget returnLabel = ExpressionFactory.Label(variable.Type);
            BodyExpressions.Add(ExpressionFactory.Return(returnLabel, variable));
            BodyExpressions.Add(ExpressionFactory.Label(returnLabel, variable));
        }

        public TDelegate Build<TDelegate>()
        {
            Expression body = ExpressionFactory.Block(BodyVariables, BodyExpressions);
            return ExpressionFactory
                .Lambda<TDelegate>(body, ParameterExpressions)
                .Compile();
        }
    }
}
