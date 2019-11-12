using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using MediatR;

namespace Gouda.Application.Commands.ExecuteCheck.Tests
{
    using Implementation;
    using Domain;
    using Gouda.Domain;
    using Communication.Abstraction;
    using Application.Template;
    using Persistence.Abstraction;
    using Analysis.Abstraction;
    using Scheduling.Abstraction;

    [TestClass]
    public class ExecuteCheckHandlerTest : MediatrTemplate
    {
        private ExecuteCheckCommand TestCommand = new ExecuteCheckCommand();
        private Check TestCheck = new Check();
        private Satellite TestSatellite = new Satellite();
        private List<DiagnosticData> TestResultData = new List<DiagnosticData>();

        private Mock<ICommunicator> MockCommunicator = new Mock<ICommunicator>();
        private Mock<IGoudaDatabase> MockGoudaDatabase = new Mock<IGoudaDatabase>();
        private Mock<IAnalyzer> MockAnalyzer = new Mock<IAnalyzer>();
        private Mock<IScheduler> MockScheduler = new Mock<IScheduler>();

        private IRequestHandler<ExecuteCheckCommand> TestObject = null;

        private Task HandleTestCommand() => TestObject.Handle(TestCommand, TestCancellationToken);

        [TestInitialize]
        public void Init()
        {
            TestCheck.Satellite = TestSatellite;
            TestCommand.Check = TestCheck;

            TestObject = new ExecuteCheckHandler(
                MockCommunicator.Object,
                MockGoudaDatabase.Object,
                MockAnalyzer.Object,
                MockScheduler.Object);

            MockCommunicator
                .Setup(coms => coms.SendCheck(It.IsAny<Check>()))
                .ReturnsAsync(TestResultData);
            MockGoudaDatabase
                .Setup(db => db.DiagnosticData.Insert(It.IsAny<List<DiagnosticData>>()));
        }

        [TestMethod]
        public async Task SendsCheckToSatellite()
        {
            await HandleTestCommand();

            MockCommunicator.Verify(coms => coms.SendCheck(TestCheck));
        }

        [TestMethod]
        public async Task InsertsResultData()
        {
            await HandleTestCommand();

            MockGoudaDatabase.Verify(db => db.DiagnosticData.Insert(TestResultData), Times.Once);
        }

        [TestMethod]
        public async Task AnalyzesResultData()
        {
            await HandleTestCommand();

            MockAnalyzer.Verify(analyzer => analyzer.AnalyzeResult(TestCheck, TestResultData), Times.Once);
        }

        [TestMethod]
        public async Task ReschedulesCheck()
        {
            await HandleTestCommand();

            MockScheduler.Verify(scheduler => scheduler.RescheduleCheck(TestCheck), Times.Once);
        }
    }
}
