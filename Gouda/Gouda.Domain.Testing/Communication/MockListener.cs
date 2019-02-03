using Gouda.Domain.Check;
using Gouda.Infrastructure.Communication;
using System.Collections.Generic;

namespace Gouda.Domain.Communication
{
    public class MockListener : Listener
    {
        public List<Request> RequestsHandled = new List<Request>();

        public MockListener()
            : base(Testing.TestEndpoint)
        {
            Handler = Handle;
        }

        private BaseResponse Handle(Request request)
        {
            RequestsHandled.Add(request);
            return new MockResponse();
        }
    }
}
