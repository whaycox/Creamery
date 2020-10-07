using Curds.Persistence.Abstraction;
using System.Threading.Tasks;

namespace Parmesan.Persistence.Abstraction
{
    using Domain;

    public interface IClientRepository : IRepository<IParmesanDataModel, Client>
    {
        Task<Client> FetchByPublicClientID(string publicClientID);
    }
}
