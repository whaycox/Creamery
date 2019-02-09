using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check
{
    public class MockRequest : Request
    {
        public MockRequest()
            : base(MockCheck.SampleID, new Dictionary<string, string>())
        { }
    }
}
