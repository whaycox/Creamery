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
        AssignIdentityDelegate AssignIdentity { get; }
        ProjectEntityDelegate<TEntity> ProjectEntity { get; }
        ValueEntityDelegate ValueEntity { get; }

        Table Table();
    }
}
