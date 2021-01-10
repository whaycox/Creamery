using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface ISimpleRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, ISimpleEntity
    {
        Task<TEntity> Fetch(int id);

        IEntityUpdate<TEntity> Update(int id);

        Task Delete(int id);
    }

    public interface ISimpleRepository<TDataModel, TEntity> : IRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : class, ISimpleEntity
    {
        Task<TEntity> Fetch(int id);

        IEntityUpdate<TEntity> Update(int id);

        Task Delete(int id);
    }
}
