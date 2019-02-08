using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;
using System.Reflection;

namespace Gouda.Infrastructure.Check.Tests
{
    [TestClass]
    public class Executor
    {
        private Request Request => new MockRequest();
        private Success Response => new MockResponse();

        private Check.Executor TestExecutor = null;

        [TestInitialize]
        public void Init()
        {
            Assembly.Load("Gouda.Domain.Testing");
            TestExecutor = new Check.Executor();
        }

        [TestMethod]
        public void ReturnsFailureOnError()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ReturnsSuccessNormally()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ProcessesHeartbeatCheck()
        {
            Assert.AreEqual(Response, TestExecutor.Perform(Request));
        }
    }
}
