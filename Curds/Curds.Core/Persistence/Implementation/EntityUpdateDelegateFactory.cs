using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Expressions.Abstraction;

    internal class EntityUpdateDelegateFactory : IEntityUpdateDelegateFactory
    {
        private IExpressionBuilderFactory ExpressionBuilderFactory { get; }

        private object Locker { get; } = new object();
        private Dictionary<Type, Dictionary<PropertyInfo, EntityUpdateDelegate>> UpdateDelegates { get; } = new Dictionary<Type, Dictionary<PropertyInfo, EntityUpdateDelegate>>();

        public EntityUpdateDelegateFactory(IExpressionBuilderFactory expressionBuilderFactory)
        {
            ExpressionBuilderFactory = expressionBuilderFactory;
        }

        public Action<TEntity> Create<TEntity, TValue>(PropertyInfo updateProperty, TValue newValue)
            where TEntity : class, IEntity
        {
            if (!updateProperty.DeclaringType.IsAssignableFrom(typeof(TEntity)))
                throw new InvalidOperationException($"Property {updateProperty.Name} does not belong to supplied entity type {typeof(TEntity).FullName}");

            EntityUpdateDelegate<TEntity, TValue> updateDelegate = FetchUpdateDelegate<TEntity, TValue>(updateProperty);
            return (TEntity entity) => updateDelegate.Update(entity, newValue);
        }
        private EntityUpdateDelegate<TEntity, TValue> FetchUpdateDelegate<TEntity, TValue>(PropertyInfo updateProperty)
            where TEntity : class, IEntity
        {
            lock (Locker)
            {
                if (!UpdateDelegates.ContainsKey(typeof(TEntity)))
                    UpdateDelegates.Add(
                        typeof(TEntity),
                        new Dictionary<PropertyInfo, EntityUpdateDelegate>());

                Dictionary<PropertyInfo, EntityUpdateDelegate> updateDelegates = UpdateDelegates[typeof(TEntity)];
                if (!updateDelegates.ContainsKey(updateProperty))
                    updateDelegates.Add(
                        updateProperty,
                        BuildUpdateDelegate<TEntity, TValue>(
                            ExpressionBuilderFactory.Create(),
                            updateProperty));

                return (EntityUpdateDelegate<TEntity, TValue>)updateDelegates[updateProperty];
            }
        }
        private EntityUpdateDelegate<TEntity, TValue> BuildUpdateDelegate<TEntity, TValue>(IExpressionBuilder expressionBuilder, PropertyInfo updateProperty)
            where TEntity : class, IEntity
        {
            ParameterExpression entityParameter = expressionBuilder.AddParameter<TEntity>(nameof(entityParameter));
            ParameterExpression valueParameter = expressionBuilder.AddParameter<TValue>(nameof(valueParameter));
            expressionBuilder.SetProperty(
                entityParameter,
                updateProperty,
                valueParameter);

            Action<TEntity, TValue> updateDelegate = expressionBuilder.Build<Action<TEntity, TValue>>();
            return new EntityUpdateDelegate<TEntity, TValue>(updateDelegate);
        }
    }
}
