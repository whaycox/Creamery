using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.AddSatellite.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class AddSatelliteValidatorTest : AddSatelliteTemplate
    {
        private AddSatelliteValidator TestObject = new AddSatelliteValidator();

        [TestMethod]
        public async Task ValidatesCommand()
        {
            await TestObject.Process(TestCommand, default);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesName(string satelliteName)
        {
            TestCommand.SatelliteName = satelliteName;

            await TestObject.Process(TestCommand, default);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("OEU")]
        [DataRow("256.255.255.255")]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesIP(string satelliteIP)
        {
            TestCommand.SatelliteIP = satelliteIP;

            await TestObject.Process(TestCommand, default);
        }
    }
}
