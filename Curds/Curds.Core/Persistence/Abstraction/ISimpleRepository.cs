using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ISimpleRepository<TModel, TEntity> : IRepository<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : SimpleEntity
    {
        Task<TEntity> Fetch(int id);

        Task Update(Action<TEntity> modifier, int id);

        Task Delete(int id);
    }
}
