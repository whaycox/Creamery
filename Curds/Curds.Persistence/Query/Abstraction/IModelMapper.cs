using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;
    using Persistence.Abstraction;

    public interface IModelMapper
    {
        Dictionary<string, Table> MapTablesByName<TModel>() where TModel : IDataModel;
        Dictionary<Type, Table> MapTablesByType<TModel>() where TModel : IDataModel;
        Dictionary<Type, ValueEntityDelegate> MapValueEntityDelegates<TModel>() where TModel : IDataModel;
    }
}
