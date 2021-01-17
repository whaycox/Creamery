using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entities);

        Task<TEntity> Fetch(params object[] keys);
        Task<List<TEntity>> FetchAll();

        IEntityUpdate<TEntity> Update(params object[] keys);

        Task Delete(params object[] keys);
    }

    public interface IRepository<TDataModel, TEntity> : IRepository<TEntity>
        where TDataModel : IDataModel
        where TEntity : class, IEntity
    { }
}
