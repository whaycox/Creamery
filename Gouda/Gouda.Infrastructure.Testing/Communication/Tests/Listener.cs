using Curds.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Gouda.Domain.Check.Responses;
using Curds.Infrastructure.Cron;
using Curds.Domain.Persistence;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Listener : CronTemplate<Communication.Listener>
    {
        private MockPersistence Persistence = null;
        private MockSender Sender = null;

        private Communication.Listener _obj = new Communication.Listener(Testing.TestEndpoint);
        protected override Communication.Listener TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
            Persistence.Reset();

            Sender = new MockSender(Persistence);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestObject.Dispose();
        }

        [TestMethod]
        public void MultipleStartsThrowsException()
        {
            TestObject.Start();
            Assert.IsTrue(TestObject.IsStarted);
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Start());
        }

        [TestMethod]
        public void StopBeforeStartThrowsException()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Stop());
        }

        [TestMethod]
        public void ReceivesProperRequest()
        {
            TestObject.Handler = ReceivesProperRequestHandler;
            TestObject.Start();
            Sender.SendTest();
        }
        private BaseResponse ReceivesProperRequestHandler(Request request)
        {
            var expectedArguments = NameValueEntity.BuildArguments(MockDefinitionArgument.Samples);

            Assert.AreEqual(MockCheck.SampleID, request.ID);
            Assert.AreEqual(expectedArguments.Count, request.Arguments.Count);
            foreach(var argPair in request.Arguments)
            {
                Assert.IsTrue(expectedArguments.ContainsKey(argPair.Key));
                Assert.AreEqual(expectedArguments[argPair.Key], argPair.Value);
            }
            return new MockResponse();
        }
        
        [TestMethod]
        public void ErrorReturnsFailureResponse()
        {
            TestObject.Handler = ErrorReturnsFailureHandler;
            TestObject.Start();
            Sender.SendTest();
            Assert.AreEqual(1, Sender.ResponsesReceived.Count);

            Failure response = Sender.ResponsesReceived[0] as Failure;
            Assert.IsTrue(response != null);
            Assert.AreEqual(FailureError, response.Error);
        }
        private BaseResponse ErrorReturnsFailureHandler(Request request) => throw new Exception(FailureError);
        private const string FailureError = "Testing Error";

    }
}
