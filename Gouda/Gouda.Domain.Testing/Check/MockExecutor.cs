using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Infrastructure.Check;

namespace Gouda.Domain.Check
{
    public class MockExecutor : Executor
    {
        public List<Request> ReceivedRequests = new List<Request>();

        public void Reset() => ReceivedRequests.Clear();
    }
}
