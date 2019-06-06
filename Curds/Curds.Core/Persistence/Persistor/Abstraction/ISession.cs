using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Security.Domain;

    public interface ISession<T> : IBaseEntity<T> where T : Session
    {
        Task<T> Lookup(string id);
        Task Update(string id, DateTimeOffset newExpiration);
        Task Delete(string series);
        Task Delete(int userID);
        Task Delete(DateTimeOffset expiration);
    }
}
