using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    public interface ISqlQueryBuilder<TDataModel>
        where TDataModel : IDataModel
    {
        ISqlQuery Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;

        ISqlUniverse<TDataModel, TEntity> From<TEntity>()
            where TEntity : class, IEntity;
    }
}
