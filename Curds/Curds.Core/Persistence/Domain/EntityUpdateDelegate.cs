using System;

namespace Curds.Persistence.Domain
{
    using Abstraction;

    public abstract class EntityUpdateDelegate
    { }

    public class EntityUpdateDelegate<TEntity, TValue> : EntityUpdateDelegate
        where TEntity : IEntity
    {
        private Action<TEntity, TValue> UpdateDelegate { get; }

        public EntityUpdateDelegate(Action<TEntity, TValue> updateDelegate)
        {
            UpdateDelegate = updateDelegate;
        }

        public void Update(TEntity entity, TValue value) => UpdateDelegate(entity, value);
    }
}
