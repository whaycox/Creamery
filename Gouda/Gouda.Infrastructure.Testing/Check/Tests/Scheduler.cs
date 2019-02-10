using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.DateTimes;
using Gouda.Domain.Persistence;
using Curds.Infrastructure.Cron;
using Gouda.Domain.Communication;
using System.Threading;
using Curds.Domain;

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
            TestScheduler.Dispose();
            MockDateTime.Reset();
        }

        [TestMethod]
        public void InvalidSleepErrors()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Check.Scheduler(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Check.Scheduler(-1));
        }

        [TestMethod]
        public void SchedulesAllDefinitionsOnStart()
        {
            TestScheduler.Start();
            Thread.Sleep(5); //give scheduler time to start its thread
            Assert.AreEqual(1, Sender.DefinitionsSent.Count);
        }



    }
}
