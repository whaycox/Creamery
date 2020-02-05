using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Abstraction;

    public class ModelEntityConfiguration<TModel, TEntity> : IModelEntityConfiguration
        where TModel : IDataModel
        where TEntity : BaseEntity
    {
        public Type ModelType => typeof(TModel);
        public Type EntityType => typeof(TEntity);

        public string Schema { get; set; }
        public string Table { get; set; }
        public string Identity { get; set; }

        public IServiceCollection ServiceCollection { get; set; }
    }
}
