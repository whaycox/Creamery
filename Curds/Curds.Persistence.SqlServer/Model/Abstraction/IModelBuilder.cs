using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    using Domain;
    using Persistence.Abstraction;

    public interface IModelBuilder
    {
        Column BuildDefaultColumn(PropertyInfo propertyInfo);
        
        Dictionary<Type, Table> TablesByType<TModel>()
            where TModel : IDataModel;
        Dictionary<Type, ValueEntityDelegate> ValueEntityDelegatesByType<TModel>()
            where TModel : IDataModel;
        Dictionary<Type, AssignIdentityDelegate> AssignIdentityDelegatesByType<TModel>()
            where TModel : IDataModel;
    }
}
