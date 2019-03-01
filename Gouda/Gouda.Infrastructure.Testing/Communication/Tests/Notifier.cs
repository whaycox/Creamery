using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Check;
using Gouda.Domain.Persistence;
using Curds.Domain.DateTimes;
using Curds.Domain;
using Gouda.Domain.Communication.ContactAdapters;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Notifier : CronTemplate<Communication.Notifier>
    {
        private MockEvaluator Evaluator = null;
        private MockPersistence Persistence = null;

        private Definition Definition => Persistence.Definitions.Lookup(MockDefinition.SampleID).GetAwaiter().GetResult();

        private Communication.Notifier _obj = null;
        protected override Communication.Notifier TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
            Persistence.Reset();

            _obj = new Communication.Notifier(Time, Persistence);

            Evaluator = new MockEvaluator(TestObject, Persistence);
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
