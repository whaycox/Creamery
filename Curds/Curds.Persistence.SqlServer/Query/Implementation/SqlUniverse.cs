using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Domain;
    using Persistence.Abstraction;
    using Model.Abstraction;

    internal class SqlUniverse<TEntity> : ISqlUniverse<TEntity>
        where TEntity : IEntity
    {
        public IEntityModel Model { get; set; }

        public ISqlQuery<TEntity> ProjectEntity() => new ProjectEntityQuery<TEntity> { Model = Model };
    }
}
