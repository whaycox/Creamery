using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Domain;

    public interface IPersistor<T> where T : BaseEntity
    {
        Task<int> Count { get; }
        Task<List<T>> FetchAll();
        Task<T> Insert(T newEntity);
    }
}
