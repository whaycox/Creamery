using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Net.Sockets;
using System.Net;

namespace Gouda.Infrastructure.Communication
{
    public class Sender : ISender
    {
        private Communicator Communicator = new Communicator();

        public Response Send(Definition definition) => Send(definition.Satellite, definition.Request);

        private Response Send(Satellite satellite, Request request)
        {
            using (TcpClient client = BuildClient(satellite))
            using (NetworkStream stream = client.GetStream())
            {
                Communicator.Send(stream, request.ToBytes());
                return Response.Parse(Communicator.ReadPacket(stream, client.ReceiveBufferSize));
            }
        }
        private TcpClient BuildClient(Satellite satellite)
        {
            IPEndPoint endpoint = satellite.Endpoint;
            string addressName = endpoint.Address.ToString();
            return new TcpClient(addressName, endpoint.Port);
        }
    }
}
