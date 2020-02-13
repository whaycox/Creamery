using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Domain;
    using Query.Domain;

    public interface IModelMap<TModel>
        where TModel : IDataModel
    {
        Table Table(string name);
        Table Table(Type type);

        ValueEntity ValueEntity<TEntity>(TEntity entity)
            where TEntity : BaseEntity;
    }
}
