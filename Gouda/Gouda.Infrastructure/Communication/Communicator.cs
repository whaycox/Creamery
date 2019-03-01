using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Infrastructure.Communication
{
    public abstract class Communicator
    {
        protected Task Send(NetworkStream stream, byte[] packet) => stream.WriteAsync(packet, 0, packet.Length);

        protected async Task<byte[]> ReadPacket(NetworkStream stream, int bufferSize)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[bufferSize];
            bytesRead = await stream.ReadAsync(buffer, 0, bufferSize);
            return buffer.Take(bytesRead).ToArray();
        }
    }
}
