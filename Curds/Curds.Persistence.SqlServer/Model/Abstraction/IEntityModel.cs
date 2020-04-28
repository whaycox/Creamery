using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Domain;
    using Query.Domain;

    public interface IEntityModel<TEntity>
        where TEntity : IEntity
    {
        AssignIdentityDelegate AssignIdentityDelegate { get; }
        ProjectEntityDelegate<TEntity> ProjectEntityDelegate { get; }

        Table Table();
        ValueEntity<TEntity> ValueEntity(TEntity entity);
    }
}
