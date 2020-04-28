using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Domain;
    using Persistence.Abstraction;

    public class EntityModel<TEntity> : IEntityModel<TEntity>
        where TEntity : IEntity
    {
        public Table _table = null;

        public AssignIdentityDelegate AssignIdentityDelegate { get; set; }
        public ProjectEntityDelegate<TEntity> ProjectEntityDelegate { get; set; }
        public ValueEntityDelegate ValueEntityDelegate { get; set; }

        public Table Table() => _table;

        public ValueEntity<TEntity> ValueEntity(TEntity entity) => ValueEntityDelegate(entity) as ValueEntity<TEntity>;
    }
}
