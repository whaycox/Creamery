using Gouda.Domain.Check;
using Gouda.Infrastructure.Communication;
using System.Collections.Generic;
using System.Threading;

namespace Gouda.Domain.Communication
{
    public class MockListener : Listener
    {
        public static Mutex Sync = new Mutex(false, nameof(MockListener));

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
