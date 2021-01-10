using System;
using System.Reflection;

namespace Curds.Persistence.Abstraction
{
    public interface IEntityUpdateDelegateFactory
    {
        Action<TEntity> Create<TEntity, TValue>(PropertyInfo updateProperty, TValue newValue)
            where TEntity : class, IEntity;
    }
}
