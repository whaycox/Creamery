using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Gouda.Communication.Tcp.Implementation
{
    using Abstraction;

    public class Sender : ISender
    {
        public Task<IConversation> BeginConversation(IPEndPoint endpoint) => Task.FromResult(BuildConversation(endpoint));
        private IConversation BuildConversation(IPEndPoint endpoint) => new Conversation(new TcpClient(endpoint.Address.ToString(), endpoint.Port));
    }
}
