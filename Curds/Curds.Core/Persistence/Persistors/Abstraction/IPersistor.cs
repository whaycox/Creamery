using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistors.Abstraction
{
    public interface IPersistor<T>
    {
        Task<int> Count();
        Task<List<T>> FetchAll();
        Task<T> Insert(T newEntity);
    }
}
