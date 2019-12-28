using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.AddCheck.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class AddCheckValidatorTest : AddCheckTemplate
    {
        private AddCheckValidator TestObject = new AddCheckValidator();

        [TestInitialize]
        public void Init()
        {
            TestCommand.Name = TestName;
            TestCommand.CheckID = TestCheckID;
        }

        [TestMethod]
        public async Task ValidatesTestCommand()
        {
            await TestObject.Process(TestCommand, default);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesSatelliteID(int testSatelliteID)
        {
            TestCommand.SatelliteID = testSatelliteID;

            await TestObject.Process(TestCommand, default);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesName(string testName)
        {
            TestCommand.Name = testName;

            await TestObject.Process(TestCommand, default);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesCheckID()
        {
            TestCommand.CheckID = Guid.Empty;

            await TestObject.Process(TestCommand, default);
        }
    }
}
