using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Clone.Implementation
{
    using Abstraction;
    using Domain;
    using Expressions.Abstraction;

    internal class CloneDefinitionFactory : ICloneDefinitionFactory
    {
        private IExpressionBuilderFactory ExpressionBuilderFactory { get; }

        private HashSet<Type> PrimitiveTypes { get; } = new HashSet<Type>
        {
            typeof(string),
            typeof(byte),
            typeof(byte?),
            typeof(short),
            typeof(short?),
            typeof(int),
            typeof(int?),
            typeof(long),
            typeof(long?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
        };
        private MethodInfo CloneMethod { get; } = typeof(ICloneFactory).GetMethod(nameof(ICloneFactory.Clone));

        public CloneDefinitionFactory(IExpressionBuilderFactory expressionBuilderFactory)
        {
            ExpressionBuilderFactory = expressionBuilderFactory;
        }

        public ICloneDefinition<TEntity> Create<TEntity>(ICloneFactory cloneFactory)
            where TEntity : class
        {
            CloneDelegate<TEntity> cloneDelegate = AssembleExpression<TEntity>(ExpressionBuilderFactory.Create());
            return new CloneDefinition<TEntity>(cloneFactory, cloneDelegate);
        }
        private CloneDelegate<TEntity> AssembleExpression<TEntity>(IExpressionBuilder expressionBuilder)
            where TEntity : class
        {
            CloneParameters parameters = new CloneParameters
            {
                SourceEntity = expressionBuilder.AddParameter<TEntity>(nameof(CloneParameters.SourceEntity)),
                TargetEntity = expressionBuilder.CreateObject<TEntity>(nameof(CloneParameters.TargetEntity)),
                CloneFactory = expressionBuilder.AddParameter<ICloneFactory>(nameof(CloneParameters.CloneFactory)),
            };
            foreach (PropertyInfo cloneableProperty in CloneableProperties(typeof(TEntity)))
                CloneProperty(expressionBuilder, parameters, cloneableProperty);

            expressionBuilder.ReturnObject(parameters.TargetEntity);

            return expressionBuilder.Build<CloneDelegate<TEntity>>();
        }
        private IEnumerable<PropertyInfo> CloneableProperties(Type cloneType) => cloneType
            .GetProperties()
            .Where(property => property.CanRead && property.CanWrite);
        private void CloneProperty(IExpressionBuilder expressionBuilder, CloneParameters parameters, PropertyInfo property)
        {
            if (IsPrimitiveProperty(property))
                ClonePrimitiveProperty(expressionBuilder, parameters, property);
            else
                CloneComplexProperty(expressionBuilder, parameters, property);
        }
        private bool IsPrimitiveProperty(PropertyInfo cloneableProperty) => PrimitiveTypes.Contains(cloneableProperty.PropertyType);
        private void ClonePrimitiveProperty(IExpressionBuilder expressionBuilder, CloneParameters parameters, PropertyInfo property)
        {
            Expression sourceValue = expressionBuilder.GetProperty(parameters.SourceEntity, property);
            expressionBuilder.SetProperty(
                parameters.TargetEntity,
                property,
                sourceValue);
        }
        private void CloneComplexProperty(IExpressionBuilder expressionBuilder, CloneParameters parameters, PropertyInfo property)
        {
            MethodInfo cloneMethod = CloneMethod.MakeGenericMethod(property.PropertyType);
            Expression complexValue = expressionBuilder.GetProperty(parameters.SourceEntity, property);
            Expression subClone = expressionBuilder.CallMethod(parameters.CloneFactory, cloneMethod, complexValue);
            expressionBuilder.SetProperty(
                parameters.TargetEntity,
                property,
                subClone);
        }
    }
}
