using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Infrastructure.Communication;
using Gouda.Domain.Check;

namespace Gouda.Domain.Communication
{
    public class MockSender : Sender
    {
        public List<BaseResponse> ResponsesReceived = new List<BaseResponse>();

        public void SendTest()
        {
            BaseResponse received = Send(Persistence.LookupDefinition(MockDefinition.SampleID));
            ResponsesReceived.Add(received);
        }
    }
}
