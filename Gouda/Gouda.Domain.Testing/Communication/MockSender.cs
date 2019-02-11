using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Infrastructure.Communication;
using Gouda.Domain.Check;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;

namespace Gouda.Domain.Communication
{
    public class MockSender : ISender
    {
        private Sender _sender = new Sender();

        public List<Definition> DefinitionsSent = new List<Definition>();
        public List<BaseResponse> ResponsesReceived = new List<BaseResponse>();

        public IPersistence Persistence { get; set; }

        public BaseResponse Send(Definition definition)
        {
            DefinitionsSent.Add(definition);
            return new MockResponse();
        }

        public void SendTest()
        {
            _sender.Persistence = Persistence;
            ResponsesReceived.Add(_sender.Send(MockDefinition.Sample));
        }
    }
}
