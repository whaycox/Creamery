using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Gouda.WebApp.Tests
{
    using Implementation;
    using Domain;
    using Application.Commands.CheckSchedule.Domain;

    [TestClass]
    public class CheckExecutionServiceTest
    {
        private int TestSleepTimeInMs = 25;
        private CancellationToken TestCancellationToken = new CancellationToken();
        private CheckExecutionServiceOptions TestOptions = new CheckExecutionServiceOptions();

        private Mock<IOptions<CheckExecutionServiceOptions>> MockOptions = new Mock<IOptions<CheckExecutionServiceOptions>>();
        private Mock<IMediator> MockMediator = new Mock<IMediator>();

        private CheckExecutionService TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestOptions.SleepTimeInMs = TestSleepTimeInMs;

            MockOptions
                .Setup(options => options.Value)
                .Returns(TestOptions);

            TestObject = new CheckExecutionService(MockOptions.Object, MockMediator.Object);
        }

        [TestMethod]
        public async Task StartIssuesCommand()
        {
            await TestObject.StartAsync(TestCancellationToken);

            await Task.Delay(5);
            MockMediator.Verify(mediator => mediator.Send(It.IsAny<CheckScheduleCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task IssuesCommandEachSleep()
        {
            int expectedCommands = 3;
            int waitDuration = ((expectedCommands - 1) * TestSleepTimeInMs) + 10;

            await TestObject.StartAsync(TestCancellationToken);

            await Task.Delay(waitDuration);
            MockMediator.Verify(mediator => mediator.Send(It.IsAny<CheckScheduleCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(expectedCommands));
        }

        [TestMethod]
        public async Task CanStopExecution()
        {
            await TestObject.StartAsync(TestCancellationToken);

            await TestObject.StopAsync(TestCancellationToken);

            await Task.Delay(TestSleepTimeInMs * 2);
            MockMediator.Verify(mediator => mediator.Send(It.IsAny<CheckScheduleCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
