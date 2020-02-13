using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Persistence.Domain;

    public class ColumnConfiguration<TEntity> : BaseColumnConfiguration
        where TEntity : BaseEntity
    {
        public EntityConfiguration<TEntity> EntityConfiguration { get; set; }

        public ColumnConfiguration(string valueName)
            : base(valueName)
        { }
    }
}
