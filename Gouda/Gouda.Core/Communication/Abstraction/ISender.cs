using System.Net;
using System.Threading.Tasks;

namespace Gouda.Communication.Abstraction
{
    public interface ISender
    {
        Task<IConversation> BeginConversation(IPEndPoint endpoint);
    }
}
