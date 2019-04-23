using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using System.Threading.Tasks;

namespace Curds.Application.Persistence.Persistor
{
    public interface IEntityPersistor<T> : IPersistor<T> where T : Entity
    {
        Task<List<T>> LookupMany(IEnumerable<int> ids);
        Task<T> Lookup(int id);

        Task Update(int id, Func<T, T> updateDelegate);
        Task Delete(int id);
    }
}
