using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    public static class Satellite
    {
        public static Communication.Satellite[] Data => new Communication.Satellite[]
        {
            Communication.MockSatellite.Sample,
        };
    }
}
