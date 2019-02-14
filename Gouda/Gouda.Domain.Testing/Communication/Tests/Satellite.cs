using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence;
using System.Net;

namespace Gouda.Domain.Communication.Tests
{
    [TestClass]
    public class Satellite : NamedEntityTemplate<Communication.Satellite>
    {
        protected override Communication.Satellite TestObject => MockSatellite.Sample;

        [TestMethod]
        public void EndpointEquality()
        {
            TestChange<IPEndPoint>((e, v) => e.Endpoint = v, null);
            TestChange((e, v) => e.Endpoint = v, new IPEndPoint(IPAddress.None, IPEndPoint.MaxPort));
        }
    }
}
