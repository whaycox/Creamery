using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Domain.Communication
{
    public class MockSatellite : Satellite
    {
        public MockSatellite()
        {
            ID = 5;
            Name = nameof(MockSatellite);
            Endpoint = Testing.TestEndpoint;
        }
    }
}
