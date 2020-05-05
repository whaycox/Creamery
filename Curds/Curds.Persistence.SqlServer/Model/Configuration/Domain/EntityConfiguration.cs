using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Model.Domain;
    using Persistence.Abstraction;

    public class EntityConfiguration<TEntity> : IEntityConfiguration
        where TEntity : IEntity
    {
        public Type EntityType => typeof(TEntity);

        public string Schema { get; set; }
        public string Table { get; set; }
        public IList<IColumnConfiguration> Columns { get; set; } = new List<IColumnConfiguration>();
        public IList<string> Keys { get; set; } = new List<string>();

        public IServiceCollection ServiceCollection { get; set; }
    }
}
