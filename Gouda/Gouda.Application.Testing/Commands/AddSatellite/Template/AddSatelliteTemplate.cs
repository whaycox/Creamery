using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Gouda.Application.Commands.AddSatellite.Template
{
    using Domain;

    public abstract class AddSatelliteTemplate
    {
        protected AddSatelliteCommand TestCommand = new AddSatelliteCommand();
        protected string TestSatelliteName = nameof(TestSatelliteName);
        protected IPAddress TestSatelliteIP = IPAddress.Parse("10.10.10.10");

        [TestInitialize]
        public void SetupCommand()
        {
            TestCommand.SatelliteName = TestSatelliteName;
            TestCommand.SatelliteIP = TestSatelliteIP.ToString();
        }
    }
}
