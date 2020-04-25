using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Domain;
    using Persistence.Abstraction;

    internal class SqlUniverse<TEntity> : ISqlUniverse<TEntity>
        where TEntity : IEntity
    {
        public Table Table { get; set; }

        public ISqlQuery<TEntity> ProjectEntity() => new ProjectEntityQuery<TEntity> { ProjectedTable = Table };
    }
}
