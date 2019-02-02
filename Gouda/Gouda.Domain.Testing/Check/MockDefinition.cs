using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Enumerations;
using Gouda.Domain.Communication;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Check
{
    public class MockDefinition : Definition<MockResponse>
    {
        protected override Definition Default => new MockDefinition();

        public override MockResponse BuildResponse(Response response) => new MockResponse(response);

        protected override Status Evaluate(MockResponse response)
        {
            if (response.CountData == 4)
                return Status.Good;
            else
                return Status.Worried;
        }
    }
}
