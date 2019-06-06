using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Domain;

    public interface IBaseEntity<T> where T : BaseEntity
    {
        Task<int> Count();
        Task<List<T>> FetchAll();
        Task<T> Insert(T newEntity);
    }
}
