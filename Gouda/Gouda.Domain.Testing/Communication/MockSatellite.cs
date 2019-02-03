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
        { }

        public const int SampleID = 1;
        public static Satellite Sample => new Satellite()
        {
            ID = SampleID,
            Name = nameof(MockSatellite),
            Endpoint = Testing.TestEndpoint,
        };
    }
}
