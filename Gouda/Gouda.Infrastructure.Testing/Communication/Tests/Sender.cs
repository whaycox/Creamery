using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Persistence;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Sender
    {
        private MockProvider Persistence = new MockProvider();
        private MockListener Listener = new MockListener();

        private Definition Definition => Persistence.LookupDefinition(MockDefinition.SampleID);

        private Communication.Sender TestSender = new Communication.Sender();

        [TestInitialize]
        public void Init()
        {
            Persistence.PopulateCache();
            TestSender.Persistence = Persistence;
            Listener.Start();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Listener.Dispose();
        }

        [TestMethod]
        public void Send()
        {
            BaseResponse expected = new MockResponse();
            BaseResponse response = TestSender.Send(Definition);
            Assert.AreEqual(1, Listener.RequestsHandled.Count);
            Assert.AreEqual(expected, response);
            Assert.AreNotSame(expected, response);
        }



    }
}
