using Curds.Persistence.Abstraction;
using System.Threading.Tasks;

namespace Parmesan.Persistence.Abstraction
{
    using Parmesan.Domain;

    public interface IUserRepository : IRepository<IParmesanDataModel, User>
    {
        Task<User> FetchByUserName(string userName);
    }
}
