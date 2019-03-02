using Gouda.Domain.Check;
using Gouda.Infrastructure.Communication;
using System.Collections.Generic;
using System.Threading;

namespace Gouda.Domain.Communication
{
    public class MockListener : Listener
    {
        public static Mutex ListenerSync = new Mutex(false, nameof(ListenerSync));

        public List<Request> RequestsHandled = new List<Request>();

        public MockListener()
            : base(Testing.TestEndpoint)
        {
            Handler = Handle;
        }

        public override void Start()
        {
            ListenerSync.WaitOne(); //Only runs on one thread at a time
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            ListenerSync.ReleaseMutex();
        }

        private BaseResponse Handle(Request request)
        {
            RequestsHandled.Add(request);
            return new MockResponse();
        }
    }
}
