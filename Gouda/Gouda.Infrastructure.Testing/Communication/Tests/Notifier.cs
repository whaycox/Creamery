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
        private Curds.Application.Cron.IProvider Cron = new Curds.Infrastructure.Cron.Provider();
        private MockEvaluator Evaluator = new MockEvaluator();
        private MockProvider Persistence = new MockProvider();

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

        [TestMethod]
        public void NotifiesOnStatusChange()
        {
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(1, MockContactOneAdapter.UsersNotified.Count);
            Assert.AreEqual(1, MockContactOneAdapter.UsersNotified[0]);
            Assert.AreEqual(1, MockContactTwoAdapter.UsersNotified.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.UsersNotified[0]);
        }

    }
}
