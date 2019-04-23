using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using System.Threading.Tasks;

namespace Curds.Application.Persistence.Persistor
{
    public interface IPersistor<T> where T : BaseEntity
    {
        Task<int> Count { get; }
        Task<List<T>> FetchAll();
        Task<T> Insert(T newEntity);
    }
}
