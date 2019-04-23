using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;
using System.Threading.Tasks;

namespace Curds.Application.Persistence.Persistor
{
    public interface ISessionPersistor<T> : IPersistor<T> where T : Session
    {
        Task<T> Lookup(string id);
        Task Update(string id, DateTimeOffset newExpiration);
        Task Delete(string series);
        Task Delete(int userID);
        Task Delete(DateTimeOffset expiration);
    }
}
