using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Persistence.Abstraction;

    public class ColumnConfiguration<TEntity, TValue> : BaseColumnConfiguration
        where TEntity : IEntity
    {
        public EntityConfiguration<TEntity> EntityConfiguration { get; set; }

        public ColumnConfiguration(string valueName)
            : base(valueName)
        { }
    }
}
