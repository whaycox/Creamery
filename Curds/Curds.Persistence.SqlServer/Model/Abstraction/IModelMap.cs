using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Domain;
    using Query.Domain;

    public interface IModelMap<TModel>
        where TModel : IDataModel
    {
        Table Table(Type type);

        ValueEntity<TEntity> ValueEntity<TEntity>(TEntity entity)
            where TEntity : IEntity;
        AssignIdentityDelegate AssignIdentityDelegate<TEntity>()
            where TEntity : IEntity;
    }
}
