using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;
    using Domain;
    using Persistence.Domain;

    public interface IModelMap<TModel>
        where TModel : IDataModel
    {
        Table Table(string name);
        Table Table(Type type);

        ValueEntity ValueEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
