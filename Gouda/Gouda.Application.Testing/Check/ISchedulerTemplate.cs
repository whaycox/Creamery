using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Gouda.Domain;
using Gouda.Domain.Persistence;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.DateTimes;
using System.Threading;

namespace Gouda.Application.Check
{
    public abstract class ISchedulerTemplate<T> : CronTemplate<T> where T : IScheduler
    {
        protected MockPersistence Persistence = new MockPersistence();
        protected MockSender Sender = new MockSender();
        protected MockDefinition Definition = MockDefinition.Sample;

        [TestInitialize]
        public void Init()
        {
            Persistence.Cron = Cron;
            Sender.Persistence = Persistence;
            TestObject.Time = Time;
            TestObject.Persistence = Persistence;
            TestObject.Sender = Sender;
        }

        [TestCleanup]
        public void Clean()
        {
            TestObject.Dispose();
        }

        [TestMethod]
        public void SchedulesAllDefinitionsOnStart()
        {
            TestObject.Start();
            Thread.Sleep(5); //give scheduler time to start
            Assert.AreEqual(1, Sender.DefinitionsSent.Count);
        }

        [TestMethod]
        public void ReschedulesCheckByDefinitionTime()
        {
            TestObject.Start();
            Thread.Sleep(5);
            Assert.AreEqual(Time.Fetch.Add(Definition.RescheduleSpan), TestObject[MockDefinition.SampleID]);
        }

        [TestMethod]
        public void ReschedulesCheckOnDemand()
        {
            TestObject.Start();
            Thread.Sleep(5);

            DateTimeOffset testTime = Time.Fetch.AddDays(1);
            TestObject.Reschedule(MockDefinition.SampleID, testTime);
            Assert.AreEqual(testTime, TestObject[MockDefinition.SampleID]);
        }

        [TestMethod]
        public void CanRemoveScheduledItem()
        {
            TestObject.Start();
            Thread.Sleep(5);

            Assert.IsNotNull(TestObject[MockDefinition.SampleID]);
            TestObject.Remove(MockDefinition.SampleID);
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject[MockDefinition.SampleID]);
        }

    }
}
