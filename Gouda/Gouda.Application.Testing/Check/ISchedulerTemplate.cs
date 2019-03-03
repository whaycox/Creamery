using Curds.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Gouda.Application.Check
{
    public abstract class ISchedulerTemplate<T> : CronTemplate<T> where T : IScheduler
    {
        private const int WaitForStartTimeInMs = 100;

        protected MockPersistence Persistence = null;
        protected MockSender Sender = null;
        protected Definition Definition = MockDefinition.Sample;

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
            Persistence.Reset();

            Sender = new MockSender(Persistence);
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
            Thread.Sleep(WaitForStartTimeInMs);
            Assert.AreEqual(1, Sender.DefinitionsSent.Count);
        }

        [TestMethod]
        public void ReschedulesCheckByDefinitionTime()
        {
            TestObject.Start();
            Thread.Sleep(WaitForStartTimeInMs);
            Assert.AreEqual(Time.Fetch.Add(Definition.RescheduleSpan), TestObject[MockDefinition.SampleID]);
        }

        [TestMethod]
        public void ReschedulesCheckOnDemand()
        {
            TestObject.Start();
            Thread.Sleep(WaitForStartTimeInMs);

            DateTimeOffset testTime = Time.Fetch.AddDays(1);
            TestObject.Reschedule(MockDefinition.SampleID, testTime);
            Assert.AreEqual(testTime, TestObject[MockDefinition.SampleID]);
        }

        [TestMethod]
        public void CanRemoveScheduledItem()
        {
            TestObject.Start();
            Thread.Sleep(WaitForStartTimeInMs);

            Assert.IsNotNull(TestObject[MockDefinition.SampleID]);
            TestObject.Remove(MockDefinition.SampleID);
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject[MockDefinition.SampleID]);
        }

    }
}
