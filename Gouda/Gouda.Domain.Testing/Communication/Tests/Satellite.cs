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
        protected override Communication.Satellite Sample => MockSatellite.Sample;

        [TestMethod]
        public void EndpointChangesCode()
        {
            var left = Sample;
            var right = Sample;
            left.Endpoint = new IPEndPoint(IPAddress.None, IPEndPoint.MaxPort);
            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestMethod]
        public void DifferentEndpointNotEquals()
        {
            var left = Sample;
            var right = Sample;
            left.Endpoint = new IPEndPoint(IPAddress.None, IPEndPoint.MaxPort);
            Assert.AreNotEqual(left, right);
        }
    }
}
