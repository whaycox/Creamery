using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ISimpleRepository<TDataModel, TEntity> : IRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : SimpleEntity
    {
        Task<TEntity> Fetch(int id);

        Task Update(Action<TEntity> modifier, int id);

        Task Delete(int id);
    }
}
