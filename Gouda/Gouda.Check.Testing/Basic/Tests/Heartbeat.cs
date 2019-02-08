using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;
using Gouda.Domain.Check.Responses;

namespace Gouda.Check.Basic.Tests
{
    [TestClass]
    public class Heartbeat
    {
        private MockRequest Request = new MockRequest();
        private MockResponse Response = new MockResponse();

        private Basic.Heartbeat TestCheck = new Basic.Heartbeat();

        [TestMethod]
        public void ReturnsGood()
        {
            Assert.AreEqual(Status.Good, TestCheck.Evaluate(Response));
        }

        [TestMethod]
        public void ReturnsEmptySuccess()
        {
            Success response = TestCheck.Perform(Request);
            Assert.AreEqual(0, response.Arguments.Count);
        }

    }
}
