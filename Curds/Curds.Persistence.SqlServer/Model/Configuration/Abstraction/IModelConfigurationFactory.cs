using System;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;

    public interface IModelConfigurationFactory
    {
        IModelEntityConfiguration Build<TModel>(Type entityType)
            where TModel : IDataModel;
    }
}
