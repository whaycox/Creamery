using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Net;
using System.Net.Sockets;

namespace Gouda.Infrastructure.Communication
{
    public class Sender : ISender
    {
        public Application.Persistence.IProvider Persistence { get; set; }

        private Communicator Communicator = new Communicator();

        public BaseResponse Send(Definition definition) => Send(LookupSatellite(definition), definition.Request);
        private Satellite LookupSatellite(Definition definition) => Persistence.LookupSatellite(definition.SatelliteID);

        private BaseResponse Send(Satellite satellite, Request request)
        {
            using (TcpClient client = BuildClient(satellite))
            using (NetworkStream stream = client.GetStream())
            {
                Communicator.Send(stream, request.ToBytes());
                return BaseResponse.Parse(Communicator.ReadPacket(stream, client.ReceiveBufferSize));
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
