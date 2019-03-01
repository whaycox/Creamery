using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Curds.Domain.DateTimes;
using Gouda.Domain.Persistence;
using Gouda.Domain.Enumerations;
using Gouda.Check.Basic;
using Gouda.Domain.Check.Responses;
using Curds.Infrastructure.Cron;
using Curds.Domain;
using Gouda.Domain.Communication.ContactAdapters;

namespace Gouda.Infrastructure.Check.Tests
{
    [TestClass]
    public class Evaluator : CronTemplate<Check.Evaluator>
    {
        private MockPersistence Persistence = null;
        private MockNotifier Notifier = null;
        private Definition Definition = MockDefinition.Sample;
        private MockResponse Response = new MockResponse();

        private Check.Evaluator _obj = null;
        protected override Check.Evaluator TestObject => _obj;

        private Heartbeat HeartbeatCheck = new Heartbeat();

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
            Persistence.Reset();

            Notifier = new MockNotifier(Time, Persistence);

            _obj = new Check.Evaluator(Notifier, Persistence);
        }

        [TestCleanup]
        public void Clean()
        {
            MockCheck.ShouldFail = false;
            MockContactOneAdapter.Notifications.Clear();
            MockContactTwoAdapter.Notifications.Clear();
        }

        [TestMethod]
        public void UpdatesStatusOnGood()
        {
            TestObject.Evaluate(Definition, Response).GetAwaiter().GetResult();
            Assert.AreNotEqual(Definition.Status, Persistence.Definitions.Lookup(Definition.ID));
            Assert.AreEqual(Status.Good, Persistence.Definitions.Lookup(Definition.ID).GetAwaiter().GetResult().Status);
        }

        [TestMethod]
        public void UpdatesStatusOnFailure()
        {
            MockCheck.ShouldFail = true;
            TestObject.Evaluate(Definition, Response).GetAwaiter().GetResult();
            Assert.AreNotEqual(Definition.Status, Persistence.Definitions.Lookup(Definition.ID).Status);
            Assert.AreEqual(Status.Critical, Persistence.Definitions.Lookup(Definition.ID).GetAwaiter().GetResult().Status);
        }

        [TestMethod]
        public void NotifiesCriticalOnFailure()
        {
            MockCheck.ShouldFail = true;
            TestObject.Evaluate(Definition, Response).GetAwaiter().GetResult();
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Critical, MockContactOneAdapter.Notifications[0].changeInformation.New);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Critical, MockContactTwoAdapter.Notifications[0].changeInformation.New);
        }

        [TestMethod]
        public void NotifiesOfGoodChange()
        {
            TestObject.Evaluate(Definition, Response).GetAwaiter().GetResult();
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Good, MockContactOneAdapter.Notifications[0].changeInformation.New);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Good, MockContactTwoAdapter.Notifications[0].changeInformation.New);
        }

        [TestMethod]
        public void ConsecutiveSameStatusDoesntNotify()
        {
            Definition.Status = Status.Good;
            TestObject.Evaluate(Definition, Response).GetAwaiter().GetResult();
            Assert.AreEqual(0, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(0, MockContactTwoAdapter.Notifications.Count);
        }

        [TestMethod]
        public void HeartbeatResponseIsGood()
        {
            Definition.CheckGuid = HeartbeatCheck.ID;
            TestObject.Evaluate(Definition, new Success()).GetAwaiter().GetResult();
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Good, MockContactOneAdapter.Notifications[0].changeInformation.New);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Good, MockContactTwoAdapter.Notifications[0].changeInformation.New);
        }

    }
}
