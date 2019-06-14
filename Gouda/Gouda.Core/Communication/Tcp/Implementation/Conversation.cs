using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Gouda.Communication.Tcp.Implementation
{
    using Abstraction;
    using Communication.Domain;
    using Enumerations;

    public class Conversation : IConversation
    {
        private TcpClient Client { get; }
        private NetworkStream Stream { get; }

        public Conversation(TcpClient client)
        {
            Client = client;
            Stream = Client.GetStream();
        }

        protected virtual Parser BuildParser(byte[] buffer) => new Parser(buffer);

        public async Task Send(ICommunicableObject communicableObject)
        {
            using (var objectStream = communicableObject.ObjectStream())
                await objectStream.CopyToAsync(Stream);
        }

        public async Task<ICommunicableObject> ReceiveObject()
        {
            byte[] received = await ReadPacket(Stream, Client.ReceiveBufferSize);
            if (received.Length == 0)
                throw new FormatException("Received a 0 length packet");

            Parser parser = BuildParser(received);
            CommunicableType type = parser.ParseType();
            return parser.ParseObject(type);
        }
        private async Task<byte[]> ReadPacket(NetworkStream stream, int bufferSize)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[bufferSize];
            bytesRead = await stream.ReadAsync(buffer, 0, bufferSize);
            return buffer.Take(bytesRead).ToArray();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
                    Stream.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
