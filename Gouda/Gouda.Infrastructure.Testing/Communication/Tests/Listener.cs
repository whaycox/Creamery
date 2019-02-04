using Curds.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Gouda.Domain.Check.Responses;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Listener : Test
    {
        private MockProvider MockProvider = new MockProvider();
        private MockSender MockSender = new MockSender();

        private Communication.Listener TestListener = new Communication.Listener(Testing.TestEndpoint);

        [TestInitialize]
        public void Init()
        {
            MockSender.Persistence = MockProvider;
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestListener.Dispose();
        }

        [TestMethod]
        public void MultipleStartsThrowsException()
        {
            TestListener.Start();
            Assert.IsTrue(TestListener.IsStarted);
            TestForException<Exception>(() => TestListener.Start());
        }

        [TestMethod]
        public void StopBeforeStartThrowsException()
        {
            TestForException<Exception>(() => TestListener.Stop());
        }

        [TestMethod]
        public void ReceivesProperRequest()
        {
            TestListener.Handler = ReceivesProperRequestHandler;
            TestListener.Start();
            MockSender.SendTest();
        }
        private BaseResponse ReceivesProperRequestHandler(Request request)
        {
            var expectedArguments = Argument.Compile(MockArgument.Samples);

            Assert.AreEqual(nameof(MockDefinition), request.Name);
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
            TestListener.Handler = ErrorReturnsFailureHandler;
            TestListener.Start();
            MockSender.SendTest();
            Assert.AreEqual(1, MockSender.ResponsesReceived.Count);

            Failure response = MockSender.ResponsesReceived[0] as Failure;
            Assert.IsTrue(response != null);
            Assert.AreEqual(FailureError, response.Error);
        }
        private BaseResponse ErrorReturnsFailureHandler(Request request) => throw new Exception(FailureError);
        private const string FailureError = "Testing Error";

    }
}
