using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Infrastructure.Communication;
using Gouda.Domain.Check;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;
using System.Threading.Tasks;
using Curds;

namespace Gouda.Domain.Communication
{
    public class MockSender : ISender
    {
        private Sender _sender = null;

        public IPersistence Persistence { get; }

        public List<Definition> DefinitionsSent = new List<Definition>();
        public List<BaseResponse> ResponsesReceived = new List<BaseResponse>();

        public MockSender(IPersistence persistence)
        {
            Persistence = persistence;
            _sender = new Sender(Persistence);
        }

        public Task<BaseResponse> Send(Definition definition)
        {
            DefinitionsSent.Add(definition);
            return Task.Factory.StartNew<BaseResponse>(() => new MockResponse());
        }

        public void SendTest()
        {
            ResponsesReceived.Add(_sender.Send(MockDefinition.Sample).AwaitResult());
        }
    }
}
