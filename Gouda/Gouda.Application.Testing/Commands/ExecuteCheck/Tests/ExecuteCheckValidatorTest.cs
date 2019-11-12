using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.ExecuteCheck.Tests
{
    using Application.Template;
    using Domain;
    using Gouda.Domain;
    using Implementation;

    [TestClass]
    public class ExecuteCheckValidatorTest : MediatrTemplate
    {
        private ExecuteCheckCommand TestCommand = new ExecuteCheckCommand();
        private Check TestCheck = new Check();
        private Satellite TestSatellite = new Satellite();

        private ExecuteCheckValidator TestObject = new ExecuteCheckValidator();

        private Task ProcessTestCommand() => TestObject.Process(TestCommand, TestCancellationToken);

        [TestInitialize]
        public void Init()
        {
            TestCheck.Satellite = TestSatellite;
            TestCommand.Check = TestCheck;
        }

        [TestMethod]
        public async Task ValidatesTestObject()
        {
            await ProcessTestCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesCheckIsntNull()
        {
            TestCommand.Check = null;

            await ProcessTestCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesCheckSatelliteIsntNull()
        {
            TestCommand.Check.Satellite = null;

            await ProcessTestCommand();
        }
    }
}
