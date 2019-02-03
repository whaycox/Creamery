using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Infrastructure.Check;

namespace Gouda.Domain.Check
{
    public class MockExecutor : BaseExecutor
    {
        public List<Request> ReceivedRequests = new List<Request>();

        public void Reset() => ReceivedRequests.Clear();

        public override BaseResponse Perform(Request request)
        {
            ReceivedRequests.Add(request);
            return new MockResponse();
        }
    }
}
