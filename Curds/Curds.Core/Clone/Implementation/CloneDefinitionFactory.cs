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
        public const string IndexPropertyName = "Item";

        private IExpressionBuilderFactory ExpressionBuilderFactory { get; }

        private IExpressionFactory ExpressionFactory { get; }

        private HashSet<Type> PrimitiveTypes { get; } = new HashSet<Type>
        {
            typeof(string),
            typeof(bool),
            typeof(bool?),
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
        private HashSet<Type> CollectionTypes { get; } = new HashSet<Type>
        {
            typeof(Array),
            typeof(List<>),
        };
        private MethodInfo CloneMethod { get; } = typeof(ICloneFactory).GetMethod(nameof(ICloneFactory.Clone));
        private PropertyInfo ArrayLengthProperty { get; } = typeof(Array).GetProperty(nameof(Array.Length));

        public CloneDefinitionFactory(
            IExpressionBuilderFactory expressionBuilderFactory,
            IExpressionFactory expressionFactory)
        {
            ExpressionBuilderFactory = expressionBuilderFactory;
            ExpressionFactory = expressionFactory;
        }

        public ICloneDefinition<TEntity> Create<TEntity>(ICloneFactory cloneFactory)
            where TEntity : class
        {
            IExpressionBuilder expressionBuilder = ExpressionBuilderFactory.Create();
            CloneDelegate<TEntity> cloneDelegate = AssembleExpression<TEntity>(expressionBuilder);

            return new CloneDefinition<TEntity>(cloneFactory, cloneDelegate);
        }
        private CloneDelegate<TEntity> AssembleExpression<TEntity>(IExpressionBuilder expressionBuilder)
            where TEntity : class
        {
            CloneExpressionContext context = new CloneExpressionContext
            {
                SourceEntity = expressionBuilder.AddParameter<TEntity>(nameof(CloneExpressionContext.SourceEntity)),
                CloneFactory = expressionBuilder.AddParameter<ICloneFactory>(nameof(CloneExpressionContext.CloneFactory)),
            };

            if (CollectionTypes.Any(collectionType => IsCollectionType(collectionType, typeof(TEntity))))
                AddCollectionExpressions<TEntity>(expressionBuilder, context);
            else
                AddCloneExpressions<TEntity>(expressionBuilder, context);

            expressionBuilder.ReturnObject(context.TargetEntity);

            return expressionBuilder.Build<CloneDelegate<TEntity>>();
        }
        private bool IsCollectionType(Type collectionType, Type entityType)
        {
            if (collectionType.IsAssignableFrom(entityType))
                return true;
            else if (entityType.IsGenericType)
                return collectionType.IsAssignableFrom(entityType.GetGenericTypeDefinition());
            return false;
        }

        private void AddCollectionExpressions<TEntity>(IExpressionBuilder expressionBuilder, CloneExpressionContext context)
        {
            if (typeof(Array).IsAssignableFrom(typeof(TEntity)))
                AddArrayExpressions<TEntity>(expressionBuilder, context);
            else
                AddListExpressions<TEntity>(expressionBuilder, context);
        }
        private void AddArrayExpressions<TEntity>(IExpressionBuilder expressionBuilder, CloneExpressionContext context)
        {
            context.TargetEntity = expressionBuilder.CreateObject<TEntity>(
                nameof(CloneExpressionContext.TargetEntity),
                new[] { typeof(int) },
                new[] { ExpressionFactory.Call(context.SourceEntity, ArrayLengthProperty.GetMethod) });
            expressionBuilder.For(context.SourceEntity, CloneArrayElementDelegate(context));
        }
        private Func<ParameterExpression, Expression> CloneArrayElementDelegate(CloneExpressionContext context)
        {
            Type elementType = context
                .SourceEntity
                .Type
                .GetElementType();
            PropertyInfo indexProperty = typeof(IList<>)
                .MakeGenericType(elementType)
                .GetProperty(IndexPropertyName);

            if (PrimitiveTypes.Contains(elementType))
                return (iterator) =>
                {
                    Expression getElementExpression = ExpressionFactory.Call(context.SourceEntity, indexProperty.GetMethod, iterator);
                    return ExpressionFactory.Call(context.TargetEntity, indexProperty.SetMethod, iterator, getElementExpression);
                };
            else
                return (iterator) =>
                {
                    Expression getElementExpression = ExpressionFactory.Call(context.SourceEntity, indexProperty.GetMethod, iterator);
                    MethodInfo cloneMethod = CloneMethod.MakeGenericMethod(elementType);
                    Expression clonedElement = ExpressionFactory.Call(context.CloneFactory, cloneMethod, getElementExpression);
                    return ExpressionFactory.Call(context.TargetEntity, indexProperty.SetMethod, iterator, clonedElement);
                };
        }
        private void AddListExpressions<TEntity>(IExpressionBuilder expressionBuilder, CloneExpressionContext context)
        {
            PropertyInfo listCountProperty = context
                .SourceEntity
                .Type
                .GetProperty(nameof(List<TEntity>.Count));
            context.TargetEntity = expressionBuilder.CreateObject<TEntity>(
                nameof(CloneExpressionContext.TargetEntity),
                new[] { typeof(int) },
                new[] { ExpressionFactory.Call(context.SourceEntity, listCountProperty.GetMethod) });
            expressionBuilder.For(context.SourceEntity, CloneListElementDelegate(expressionBuilder, context));
        }
        private Func<ParameterExpression, Expression> CloneListElementDelegate(IExpressionBuilder expressionBuilder, CloneExpressionContext context)
        {
            Type elementType = context
                .SourceEntity
                .Type
                .GenericTypeArguments
                .Single();
            PropertyInfo indexProperty = typeof(IList<>)
                .MakeGenericType(elementType)
                .GetProperty(IndexPropertyName);
            MethodInfo addMethod = typeof(ICollection<>)
                .MakeGenericType(elementType)
                .GetMethod(nameof(ICollection<object>.Add));

            if (PrimitiveTypes.Contains(elementType))
                return (iterator) =>
                {
                    Expression getElementExpression = ExpressionFactory.Call(context.SourceEntity, indexProperty.GetMethod, iterator);
                    return ExpressionFactory.Call(context.TargetEntity, addMethod, getElementExpression);
                };
            else
                return (iterator) =>
                {
                    Expression getElementExpression = ExpressionFactory.Call(context.SourceEntity, indexProperty.GetMethod, iterator);
                    MethodInfo cloneMethod = CloneMethod.MakeGenericMethod(elementType);
                    Expression clonedElement = ExpressionFactory.Call(context.CloneFactory, cloneMethod, getElementExpression);
                    return ExpressionFactory.Call(context.TargetEntity, addMethod, clonedElement);
                };
        }

        private void AddCloneExpressions<TEntity>(IExpressionBuilder expressionBuilder, CloneExpressionContext parameters)
        {
            parameters.TargetEntity = expressionBuilder.CreateObject<TEntity>(nameof(CloneExpressionContext.TargetEntity));
            foreach (PropertyInfo cloneableProperty in CloneableProperties(typeof(TEntity)))
                CloneProperty(expressionBuilder, parameters, cloneableProperty);
        }
        private IEnumerable<PropertyInfo> CloneableProperties(Type cloneType) => cloneType
            .GetProperties()
            .Where(property => property.CanRead && property.CanWrite);
        private void CloneProperty(IExpressionBuilder expressionBuilder, CloneExpressionContext parameters, PropertyInfo property)
        {
            if (PrimitiveTypes.Contains(property.PropertyType))
                ClonePrimitiveProperty(expressionBuilder, parameters, property);
            else
                CloneComplexProperty(expressionBuilder, parameters, property);
        }
        private void ClonePrimitiveProperty(IExpressionBuilder expressionBuilder, CloneExpressionContext parameters, PropertyInfo property)
        {
            Expression sourceValue = ExpressionFactory.Call(parameters.SourceEntity, property.GetMethod);
            expressionBuilder.SetProperty(
                parameters.TargetEntity,
                property,
                sourceValue);
        }
        private void CloneComplexProperty(IExpressionBuilder expressionBuilder, CloneExpressionContext parameters, PropertyInfo property)
        {
            MethodInfo cloneMethod = CloneMethod.MakeGenericMethod(property.PropertyType);
            Expression complexValue = ExpressionFactory.Call(parameters.SourceEntity, property.GetMethod);
            Expression subClone = ExpressionFactory.Call(parameters.CloneFactory, cloneMethod, complexValue);
            expressionBuilder.SetProperty(
                parameters.TargetEntity,
                property,
                subClone);
        }
    }
}
