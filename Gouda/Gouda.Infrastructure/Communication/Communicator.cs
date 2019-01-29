using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace Gouda.Infrastructure.Communication
{
    internal class Communicator
    {
        public void Send(NetworkStream stream, byte[] packet) => stream.Write(packet, 0, packet.Length);

        public byte[] ReadPacket(NetworkStream stream, int bufferSize)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[bufferSize];
            bytesRead = stream.Read(buffer, 0, bufferSize);
            return buffer.Take(bytesRead).ToArray();
        }
    }
}
