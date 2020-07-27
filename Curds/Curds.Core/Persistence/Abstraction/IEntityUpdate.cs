using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IEntityUpdate<TEntity> : IExecutableObject
        where TEntity : IEntity
    {
        IEntityUpdate<TEntity> Set<TValue>(Expression<Func<TEntity, TValue>> propertyExpression, TValue newValue);
    }
}
