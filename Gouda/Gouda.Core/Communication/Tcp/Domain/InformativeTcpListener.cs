using System.Net;
using System.Net.Sockets;

namespace Gouda.Communication.Tcp.Domain
{
    public class InformativeTcpListener : TcpListener
    {
        public bool IsActive => Active;

        public InformativeTcpListener(IPEndPoint endpoint)
            : base(endpoint)
        { }
    }
}
