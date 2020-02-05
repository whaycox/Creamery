using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class CompiledConfiguration<TModel> : IModelEntityConfiguration
        where TModel : IDataModel
    {
        public Type ModelType => typeof(TModel);
        public Type EntityType { get; }

        public string Schema { get; set; }
        public string Table { get; set; }
        public string Identity { get; set; }

        public CompiledConfiguration(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
