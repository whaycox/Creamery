using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Model.Domain;
    using Persistence.Abstraction;
    using Model.Abstraction;

    internal class CompiledConfiguration<TModel> : IModelEntityConfiguration
        where TModel : IDataModel
    {
        public Type ModelType => typeof(TModel);
        public Type EntityType { get; }

        public string Schema { get; set; }
        public string Table { get; set; }

        public Dictionary<string, CompiledColumnConfiguration<TModel>> Columns { get; set; } = new Dictionary<string, CompiledColumnConfiguration<TModel>>();
        IList<IColumnConfiguration> IEntityConfiguration.Columns => Columns.Values
            .Cast<IColumnConfiguration>()
            .ToList();

        public List<string> Keys { get; } = new List<string>();
        IList<string> IEntityConfiguration.Keys => Keys;

        public CompiledConfiguration(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
