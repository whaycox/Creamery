using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Security.Domain;

    public interface IReAuth<T> : IBaseEntity<T> where T : ReAuth
    {
        Task<T> Lookup(string series);
        Task<List<T>> Lookup(int userID);
        Task Update(string series, string newToken);
        Task Delete(string series);
        Task Delete(int userID);
    }
}
