using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Model.Domain;
    using Query.Domain;

    public interface IModelEntity<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        Table Table();

        ValueEntity<TEntity> ValueEntity(TEntity entity);
        AssignIdentityDelegate AssignIdentityDelegate();
    }
}
