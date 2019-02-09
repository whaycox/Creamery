using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Net;
using System.Net.Sockets;

namespace Gouda.Infrastructure.Communication
{
    public class Sender : Communicator, ISender
    {
        public Application.Persistence.IPersistence Persistence { get; set; }

        public BaseResponse Send(Definition definition) => Send(LookupSatellite(definition), BuildRequest(definition));
        private Satellite LookupSatellite(Definition definition) => Persistence.Satellites.Lookup(definition.SatelliteID);
        private Request BuildRequest(Definition definition) => new Request(definition.CheckID, Argument.Compile(Persistence.GenerateArguments(definition.ID)));

        private BaseResponse Send(Satellite satellite, Request request)
        {
            using (TcpClient client = BuildClient(satellite))
            using (NetworkStream stream = client.GetStream())
            {
                Send(stream, request.ToBytes());
                return BaseResponse.Parse(ReadPacket(stream, client.ReceiveBufferSize));
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
