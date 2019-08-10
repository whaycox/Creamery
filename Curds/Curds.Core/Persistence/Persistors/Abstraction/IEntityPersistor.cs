using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistors.Abstraction
{
    using Persistence.Abstraction;

    public interface IEntityPersistor<T> : IPersistor<T> where T : IEntity
    {
        Task<List<T>> LookupMany(IEnumerable<int> ids);
        Task<T> Lookup(int id);

        Task Update(int id, Func<T, T> updateDelegate);
        Task Delete(int id);
    }
}
