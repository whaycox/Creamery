using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ISimpleRepository<TDataModel, TEntity> : IRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : SimpleEntity
    {
        Task<TEntity> Fetch(int id);

        IEntityUpdate<TEntity> Update(int id);

        Task Delete(int id);
    }
}
