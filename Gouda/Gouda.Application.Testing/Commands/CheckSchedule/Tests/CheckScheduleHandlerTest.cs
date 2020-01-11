using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using MediatR;
using Curds.Time.Abstraction;

namespace Gouda.Application.Commands.CheckSchedule.Tests
{
    using Implementation;
    using Domain;
    using Scheduling.Abstraction;
    using Gouda.Domain;
    using ExecuteCheck.Domain;

    [TestClass]
    public class CheckScheduleHandlerTest
    {
        private DateTimeOffset TestTime = new DateTimeOffset(2001, 10, 1, 13, 43, 24, TimeSpan.Zero);
        private CheckDefinition TestCheck = new CheckDefinition();
        private CheckScheduleCommand TestCommand = new CheckScheduleCommand();

        private Mock<ITime> MockTime = new Mock<ITime>();
        private Mock<IScheduler> MockScheduler = new Mock<IScheduler>();
        private Mock<IMediator> MockMediator = new Mock<IMediator>();

        private IRequestHandler<CheckScheduleCommand> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new CheckScheduleHandler(
                MockTime.Object,
                MockScheduler.Object,
                MockMediator.Object);

            MockTime
                .Setup(time => time.Current)
                .Returns(TestTime);
            MockScheduler
                .Setup(scheduler => scheduler.ChecksBeforeScheduledTime(It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(new List<CheckDefinition> { TestCheck });
        }

        [TestMethod]
        public async Task RequestsCurrentTime()
        {
            await TestObject.Handle(TestCommand, default);

            MockTime.Verify(time => time.Current, Times.Once);
        }

        [TestMethod]
        public async Task FetchesScheduledChecks()
        {
            await TestObject.Handle(TestCommand, default);

            MockScheduler.Verify(scheduler => scheduler.ChecksBeforeScheduledTime(TestTime), Times.Once);
        }

        [TestMethod]
        public async Task ExecutesScheduledChecks()
        {
            await TestObject.Handle(TestCommand, default);

            MockMediator.Verify(mediator => mediator.Send(It.Is<ExecuteCheckCommand>(command => ReferenceEquals(TestCheck, command.Check)), default), Times.Once);
        }
    }
}
