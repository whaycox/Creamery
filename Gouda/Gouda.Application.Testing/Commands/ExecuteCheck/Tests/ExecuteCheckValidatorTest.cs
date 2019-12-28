using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.ExecuteCheck.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class ExecuteCheckValidatorTest : ExecuteCheckTemplate
    {
        private ExecuteCheckValidator TestObject = new ExecuteCheckValidator();

        private Task ProcessTestCommand() => TestObject.Process(TestCommand, default);

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
