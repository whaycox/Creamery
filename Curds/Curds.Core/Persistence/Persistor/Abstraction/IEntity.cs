using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Domain;

    public interface IEntity<T> : IBaseEntity<T> where T : Entity
    {
        Task<List<T>> LookupMany(IEnumerable<int> ids);
        Task<T> Lookup(int id);

        Task Update(int id, Func<T, T> updateDelegate);
        Task Delete(int id);
    }
}
