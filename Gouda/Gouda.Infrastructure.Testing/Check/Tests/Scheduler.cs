using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.DateTimes;
using Gouda.Domain.Persistence;
using Curds.Infrastructure.Cron;
using Gouda.Domain.Communication;

namespace Gouda.Infrastructure.Check.Tests
{
    [TestClass]
    public class Scheduler
    {
        private MockDateTime Time = new MockDateTime();
        private CronProvider Cron = new CronProvider();
        private MockPersistence Persistence = new MockPersistence();
        private MockSender Sender = new MockSender();

        private Check.Scheduler TestScheduler = new Check.Scheduler();


        [TestInitialize]
        public void Init()
        {
            Persistence.Cron = Cron;
            Sender.Persistence = Persistence;
            TestScheduler.Time = Time;
            TestScheduler.Persistence = Persistence;
            TestScheduler.Sender = Sender;
        }

        [TestCleanup]
        public void Clean()
        {
            MockDateTime.Reset();
        }

        [TestMethod]
        public void SchedulesAllDefinitionsOnStart()
        {
            TestScheduler.Start();
            Assert.AreEqual(1, Sender.DefinitionsSent.Count);
        }

    }
}
