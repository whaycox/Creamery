using Microsoft.Extensions.DependencyInjection;
using System;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;

    public class EntityConfiguration<TEntity> : IEntityConfiguration
        where TEntity : BaseEntity
    {
        public Type EntityType => typeof(TEntity);

        public string Table { get; set; }
        public string Identity { get; set; }
        public string Schema { get; set; }

        public IServiceCollection ServiceCollection { get; set; }
    }
}
