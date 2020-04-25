using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface IRepository<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entities);

        Task<TEntity> Fetch(params object[] keys);
        Task<List<TEntity>> FetchAll();

        Task Update(Action<TEntity> modifier, params object[] keys);

        Task Delete(params object[] keys);
    }
}
