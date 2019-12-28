using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.ExecuteCheck.Tests
{
    using Analysis.Abstraction;
    using Communication.Abstraction;
    using Domain;
    using Gouda.Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Scheduling.Abstraction;
    using Template;

    [TestClass]
    public class ExecuteCheckHandlerTest : ExecuteCheckTemplate
    {
        private List<DiagnosticData> TestResultData = new List<DiagnosticData>();

        private Mock<ICommunicator> MockCommunicator = new Mock<ICommunicator>();
        private Mock<IGoudaDatabase> MockGoudaDatabase = new Mock<IGoudaDatabase>();
        private Mock<IAnalyzer> MockAnalyzer = new Mock<IAnalyzer>();
        private Mock<IScheduler> MockScheduler = new Mock<IScheduler>();

        private IRequestHandler<ExecuteCheckCommand> TestObject = null;

        private Task HandleTestCommand() => TestObject.Handle(TestCommand, default);

        [TestInitialize]
        public void Init()
        {
            MockCommunicator
                .Setup(coms => coms.SendCheck(It.IsAny<CheckDefinition>()))
                .ReturnsAsync(TestResultData);
            MockGoudaDatabase
                .Setup(db => db.DiagnosticData.Insert(It.IsAny<List<DiagnosticData>>()));

            TestObject = new ExecuteCheckHandler(
                MockCommunicator.Object,
                MockGoudaDatabase.Object,
                MockAnalyzer.Object,
                MockScheduler.Object);
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
