using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Model.Domain;
    using Persistence.Domain;

    public class EntityConfiguration<TEntity> : IEntityConfiguration
        where TEntity : BaseEntity
    {
        public Type EntityType => typeof(TEntity);

        public string Schema { get; set; }
        public string Table { get; set; }
        public List<IColumnConfiguration> Columns { get; set; } = new List<IColumnConfiguration>();

        public IServiceCollection ServiceCollection { get; set; }
    }
}
