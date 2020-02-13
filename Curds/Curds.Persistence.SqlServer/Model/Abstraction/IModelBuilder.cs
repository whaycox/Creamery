using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Domain;
    using Persistence.Abstraction;

    public interface IModelBuilder
    {
        Dictionary<string, Table> TablesByName<TModel>()
            where TModel : IDataModel;
        Dictionary<Type, Table> TablesByType<TModel>()
            where TModel : IDataModel;
        Dictionary<Type, ValueEntityDelegate> ValueEntityDelegatesByType<TModel>()
            where TModel : IDataModel;
    }
}
