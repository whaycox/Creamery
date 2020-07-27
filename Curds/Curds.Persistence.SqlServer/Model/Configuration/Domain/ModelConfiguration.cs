using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Persistence.Abstraction;
    using Abstraction;
    using Model.Abstraction;

    public class ModelConfiguration<TModel> : IModelConfiguration
        where TModel : IDataModel
    {
        public Type ModelType => typeof(TModel);

        public string Schema { get; set; }
    }
}
