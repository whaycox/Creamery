using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Query.Domain;
    using Persistence.Domain;

    public interface ITypeMapper
    {
        IEnumerable<(string tableName, Type tableType)> TableTypes<TModel>() 
            where TModel : IDataModel;

        IEnumerable<PropertyInfo> ValueTypes(Type entityType);
    }
}
