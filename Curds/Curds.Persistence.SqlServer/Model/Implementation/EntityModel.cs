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

        public AssignIdentityDelegate AssignIdentity { get; set; }
        public ProjectEntityDelegate<TEntity> ProjectEntity { get; set; }
        public ValueEntityDelegate ValueEntity { get; set; }

        public Table Table() => _table;
    }
}
