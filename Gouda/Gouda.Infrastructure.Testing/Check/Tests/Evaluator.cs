using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Curds.Domain.DateTimes;
using Gouda.Domain.Persistence;
using Gouda.Domain.Communication.Contacts.Adapters;
using Gouda.Domain.Enumerations;
using Gouda.Check.Basic;
using Gouda.Domain.Check.Responses;
using Curds.Infrastructure.Cron;

namespace Gouda.Infrastructure.Check.Tests
{
    [TestClass]
    public class Evaluator
    {
        private MockDateTime Time = new MockDateTime();
        private CronProvider Cron = new CronProvider();
        private MockPersistence Persistence = new MockPersistence();
        private MockNotifier Notifier = new MockNotifier();
        private MockDefinition Definition = MockDefinition.Sample;
        private MockResponse Response = new MockResponse();

        private Check.Evaluator TestEvaluator = null;

        private Heartbeat HeartbeatCheck = new Heartbeat();

        [TestInitialize]
        public void Init()
        {
            TestEvaluator = new Check.Evaluator(Notifier, Persistence);
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
            TestEvaluator.Evaluate(Definition, Response);
            Assert.AreNotEqual(Definition.Status, Persistence.Definitions.Lookup(Definition.ID));
            Assert.AreEqual(Status.Good, Persistence.Definitions.Lookup(Definition.ID).Status);
        }

        [TestMethod]
        public void UpdatesStatusOnFailure()
        {
            MockCheck.ShouldFail = true;
            TestEvaluator.Evaluate(Definition, Response);
            Assert.AreNotEqual(Definition.Status, Persistence.Definitions.Lookup(Definition.ID).Status);
            Assert.AreEqual(Status.Critical, Persistence.Definitions.Lookup(Definition.ID).Status);
        }

        [TestMethod]
        public void NotifiesCriticalOnFailure()
        {
            MockCheck.ShouldFail = true;
            TestEvaluator.Evaluate(Definition, Response);
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
            TestEvaluator.Evaluate(Definition, Response);
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
            TestEvaluator.Evaluate(Definition, Response);
            Assert.AreEqual(0, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(0, MockContactTwoAdapter.Notifications.Count);
        }

        [TestMethod]
        public void HeartbeatResponseIsGood()
        {
            Definition.CheckID = HeartbeatCheck.ID;
            TestEvaluator.Evaluate(Definition, new Success());
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Good, MockContactOneAdapter.Notifications[0].changeInformation.New);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications[0].userNotified);
            Assert.AreEqual(Status.Good, MockContactTwoAdapter.Notifications[0].changeInformation.New);
        }

    }
}
