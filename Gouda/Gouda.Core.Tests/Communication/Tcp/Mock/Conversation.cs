using System.Net.Sockets;

namespace Gouda.Communication.Tcp.Mock
{
    using Communication.Domain;

    public class Conversation : Implementation.Conversation
    {
        public Conversation(TcpClient client)
            : base(client)
        { }

        protected override Parser BuildParser(byte[] buffer) => new Communication.Mock.Parser(buffer);
    }
}
