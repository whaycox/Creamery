using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : IEntity
    {
        Task Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entities);

        Task<TEntity> Fetch(params object[] keys);
        Task<IList<TEntity>> FetchAll();

        Task Update(Action<TEntity> modifier, params object[] keys);

        Task Delete(params object[] keys);
    }
}
