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
using FluentValidation;

namespace Gouda.Application.Commands.AddSatellite.Tests
{
    using Implementation;
    using Domain;

    [TestClass]
    public class AddSatelliteValidatorTest
    {
        private string TestSatelliteName = nameof(TestSatelliteName);
        private string TestSatelliteIP = "0.0.0.0";
        private AddSatelliteCommand TestCommand = new AddSatelliteCommand();

        private AddSatelliteValidator TestObject = new AddSatelliteValidator();

        [TestInitialize]
        public void Init()
        {
            TestCommand.SatelliteName = TestSatelliteName;
            TestCommand.SatelliteIP = TestSatelliteIP;
        }

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
