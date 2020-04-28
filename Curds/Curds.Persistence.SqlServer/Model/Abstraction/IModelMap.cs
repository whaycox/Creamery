using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Domain;
    using Query.Domain;

    public interface IModelMap<TModel>
        where TModel : IDataModel
    {
        IEntityModel<TEntity> Entity<TEntity>()
            where TEntity : IEntity;
    }
}
