using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Abstraction;
    using Model.Domain;
    using Model.Abstraction;

    public class ModelEntityConfiguration<TModel, TEntity> : IModelEntityConfiguration
        where TModel : IDataModel
        where TEntity : IEntity
    {
        public Type ModelType => typeof(TModel);
        public Type EntityType => typeof(TEntity);

        public string Schema { get; set; }
        public string Table { get; set; }
        public IList<IColumnConfiguration> Columns { get; set; } = new List<IColumnConfiguration>();
        public IList<string> Keys { get; set; } = new List<string>();

        public IServiceCollection ServiceCollection { get; set; }
    }
}
