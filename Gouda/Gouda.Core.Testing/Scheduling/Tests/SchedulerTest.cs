using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curds.Time.Abstraction;

namespace Gouda.Scheduling.Tests
{
    using Abstraction;
    using Gouda.Domain;
    using Implementation;
    using Persistence.Abstraction;

    [TestClass]
    public class SchedulerTest
    {
        private DateTimeOffset TestTime = new DateTimeOffset(2003, 3, 16, 2, 4, 34, TimeSpan.FromMinutes(30));
        private List<int> TestCheckIDs = new List<int>();
        private CheckDefinition TestCheck = new CheckDefinition();
        private int TestCheckID = 13;
        private int TestRescheduleInterval = 30;
        private List<CheckDefinition> TestChecks = new List<CheckDefinition>();

        private Mock<ITime> MockTime = new Mock<ITime>();
        private Mock<IScheduleFactory> MockScheduleFactory = new Mock<IScheduleFactory>();
        private Mock<ISchedule> MockSchedule = new Mock<ISchedule>();
        private Mock<ICheckInheritor> MockCheckInheritor = new Mock<ICheckInheritor>();

        private Scheduler TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestCheck.ID = TestCheckID;
            TestCheck.RescheduleSecondInterval = TestRescheduleInterval;

            MockTime
                .Setup(time => time.Current)
                .Returns(TestTime);
            MockScheduleFactory
                .Setup(factory => factory.BuildSchedule())
                .Returns(MockSchedule.Object);
            MockSchedule
                .Setup(schedule => schedule.Trim(It.IsAny<DateTimeOffset>()))
                .Returns(TestCheckIDs);
            MockCheckInheritor
                .Setup(check => check.Build(It.IsAny<List<int>>()))
                .ReturnsAsync(TestChecks);

            TestObject = new Scheduler(
                MockTime.Object,
                MockScheduleFactory.Object,
                MockCheckInheritor.Object);
        }

        [TestMethod]
        public async Task ChecksBeforeTimeTrimsSchedule()
        {
            await TestObject.ChecksBeforeScheduledTime(TestTime);

            MockSchedule.Verify(sched => sched.Trim(TestTime), Times.Once);
        }

        [TestMethod]
        public async Task ChecksBeforeTimeFetchesInheritedChecks()
        {
            await TestObject.ChecksBeforeScheduledTime(TestTime);

            MockCheckInheritor.Verify(check => check.Build(TestCheckIDs), Times.Once);
        }

        [TestMethod]
        public void RescheduleAddsToSchedule()
        {
            DateTimeOffset expectedRescheduleTime = TestTime.AddSeconds(TestRescheduleInterval);

            TestObject.RescheduleCheck(TestCheck);

            MockSchedule.Verify(schedule => schedule.Add(TestCheckID, expectedRescheduleTime), Times.Once);
        }
    }
}
