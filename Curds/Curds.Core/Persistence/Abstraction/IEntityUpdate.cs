using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    public interface IEntityUpdate<TEntity> : IExecutableObject
        where TEntity : class, IEntity
    {
        IEntityUpdate<TEntity> Set<TValue>(Expression<Func<TEntity, TValue>> propertyExpression, TValue newValue);
    }
}
