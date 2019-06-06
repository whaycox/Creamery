using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Abstraction
{
    using Security.Domain;

    public interface IUser<T> : IEntity<T> where T : User
    {
        Task<User> FindByEmail(string email);
    }
}
