using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Abstraction;
    using Model.Abstraction;

    public class ModelColumnConfiguration<TModel, TEntity, TValue> : BaseColumnConfiguration
        where TModel : IDataModel
        where TEntity : IEntity
    {
        public ModelEntityConfiguration<TModel, TEntity> EntityConfiguration { get; set; }

        public ModelColumnConfiguration(string valueName)
            : base(valueName)
        { }
    }
}
