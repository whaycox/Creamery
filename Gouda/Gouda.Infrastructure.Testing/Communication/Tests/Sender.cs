using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Persistence;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;
using Curds.Domain;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Sender : CronTemplate<Communication.Sender>
    {
        private MockPersistence Persistence = null;
        private MockListener Listener = new MockListener();

        private Definition Definition => Persistence.Definitions.Lookup(MockDefinition.SampleID).GetAwaiter().GetResult();

        private Communication.Sender _obj = null;
        protected override Communication.Sender TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
            Persistence.Reset();

            _obj = new Communication.Sender(Persistence);

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
            BaseResponse response = TestObject.Send(Definition).GetAwaiter().GetResult();
            Assert.AreEqual(1, Listener.RequestsHandled.Count);
            Assert.AreEqual(expected, response);
            Assert.AreNotSame(expected, response);
        }



    }
}
