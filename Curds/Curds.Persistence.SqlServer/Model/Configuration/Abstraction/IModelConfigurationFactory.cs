using System;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;

    internal interface IModelConfigurationFactory
    {
        CompiledConfiguration<TModel> Build<TModel>(Type entityType)
            where TModel : IDataModel;
    }
}
