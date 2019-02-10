using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Check;
using Gouda.Domain.Persistence;
using Curds.Domain.DateTimes;
using Gouda.Domain.Communication.Contacts.Adapters;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Notifier
    {
        private MockDateTime Time = new MockDateTime();
        private Curds.Application.Cron.ICron Cron = new Curds.Infrastructure.Cron.CronProvider();
        private MockEvaluator Evaluator = new MockEvaluator();
        private MockPersistence Persistence = new MockPersistence();

        private Definition Definition => Persistence.Definitions.Lookup(MockDefinition.SampleID);

        private Communication.Notifier TestNotifier = new Communication.Notifier();

        [TestInitialize]
        public void Init()
        {
            Persistence.Cron = Cron;
            Evaluator.Notifier = TestNotifier;
            TestNotifier.Time = Time;
            TestNotifier.Persistence = Persistence;

            Persistence.LoadRelationships();
        }

        [TestCleanup]
        public void Clean()
        {
            MockContactOneAdapter.Notifications.Clear();
            MockContactTwoAdapter.Notifications.Clear();
        }

        [TestMethod]
        public void NotifiesOnStatusChange()
        {
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications[0].userNotified);
        }

    }
}
