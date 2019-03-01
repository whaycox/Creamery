using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using Curds.Domain.Persistence;
using System.Threading.Tasks;
using Gouda.Application.Persistence;

namespace Gouda.Infrastructure.Communication
{
    public class Sender : Communicator, ISender
    {
        public IPersistence Persistence { get; }

        public Sender(IPersistence persistence)
        {
            Persistence = persistence;
        }

        public async Task<BaseResponse> Send(Definition definition) => await Send(await LookupSatellite(definition), await BuildRequest(definition));
        private async Task<Satellite> LookupSatellite(Definition definition) => await Persistence.Satellites.Lookup(definition.SatelliteID);
        private async Task<Request> BuildRequest(Definition definition) => new Request(definition.CheckGuid, NameValueEntity.BuildArguments(await Persistence.GenerateArguments(definition.ID)));

        private async Task<BaseResponse> Send(Satellite satellite, Request request)
        {
            using (TcpClient client = BuildClient(satellite))
            using (NetworkStream stream = client.GetStream())
            {
                await Send(stream, request.ToBytes());
                return BaseResponse.Parse(await ReadPacket(stream, client.ReceiveBufferSize));
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
