using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using System.Threading.Tasks;

namespace Curds.Application.Persistence
{
    public interface IPersistor<T> where T : Entity
    {
        Task<int> Count { get; }

        Task<List<T>> FetchAll();

        Task<T> Lookup(int id);

        Task<T> Insert(T newEntity);

        Task Update(int id, Func<T, T> updateDelegate);

        Task Delete(int id);
    }
}
