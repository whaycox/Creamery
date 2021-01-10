using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Expressions.Abstraction;

    internal class SimpleEntityUpdate<TEntity> : IEntityUpdate<TEntity>
        where TEntity : class, ISimpleEntity
    {
        public TEntity UpdatingEntity { get; }
        public IExpressionParser ExpressionParser { get; }
        public IEntityUpdateDelegateFactory UpdateDelegateFactory { get; }

        private List<Action<TEntity>> Updates { get; } = new List<Action<TEntity>>();

        public SimpleEntityUpdate(
            TEntity updatingEntity,
            IExpressionParser expressionParser,
            IEntityUpdateDelegateFactory updateDelegateFactory)
        {
            UpdatingEntity = updatingEntity;
            ExpressionParser = expressionParser;
            UpdateDelegateFactory = updateDelegateFactory;
        }

        public IEntityUpdate<TEntity> Set<TValue>(Expression<Func<TEntity, TValue>> propertyExpression, TValue newValue)
        {
            PropertyInfo updateProperty = ExpressionParser.ParsePropertyExpression(propertyExpression);
            if (!updateProperty.CanWrite)
                throw new InvalidOperationException("Cannot set a read-only property");

            Updates.Add(
                UpdateDelegateFactory.Create<TEntity, TValue>(updateProperty, newValue));
            return this;
        }

        public Task Execute()
        {
            foreach (Action<TEntity> updateDelegate in Updates)
                updateDelegate(UpdatingEntity);
            return Task.CompletedTask;
        }
    }
}
