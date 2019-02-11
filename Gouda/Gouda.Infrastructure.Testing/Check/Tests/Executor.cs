using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;
using System.Reflection;
using Curds.Domain;
using Gouda.Check.Basic;

namespace Gouda.Infrastructure.Check.Tests
{
    [TestClass]
    public class Executor
    {
        private MockRequest Request => new MockRequest();
        private MockResponse Response => new MockResponse();

        private Heartbeat HeartbeatCheck = new Heartbeat();
        private Request HeartbeatRequest => new Request(HeartbeatCheck.ID, new Dictionary<string, string>());

        private Check.Executor TestExecutor = null;

        [TestInitialize]
        public void Init()
        {
            Assembly.Load($"{nameof(Gouda)}.{nameof(Domain)}.{nameof(Testing)}");
            TestExecutor = new Check.Executor();
        }

        [TestCleanup]
        public void Clean()
        {
            MockCheck.ShouldFail = false;
        }

        [TestMethod]
        public void ReturnsFailureOnError()
        {
            MockCheck.ShouldFail = true;
            BaseResponse response = TestExecutor.Perform(Request);
            Assert.IsTrue(response is Failure);
        }

        [TestMethod]
        public void ReturnsSuccessNormally()
        {
            Assert.IsTrue(TestExecutor.Perform(Request) is Success);
        }

        [TestMethod]
        public void ProcessesHeartbeatCheck()
        {
            Assert.AreEqual(new Success(), TestExecutor.Perform(HeartbeatRequest));
        }
    }
}
