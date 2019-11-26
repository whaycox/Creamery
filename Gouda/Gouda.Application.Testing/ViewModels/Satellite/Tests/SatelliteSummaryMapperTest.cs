using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Net;

namespace Gouda.Application.ViewModels.Satellite.Tests
{
    using Implementation;
    using Gouda.Domain;
    using Domain;
    using Enumerations;

    [TestClass]
    public class SatelliteSummaryMapperTest
    {
        private Satellite TestSatellite = new Satellite();
        private int TestSatelliteID = 3;
        private string TestSatelliteName = nameof(TestSatelliteName);
        private IPAddress TestSatelliteIP = IPAddress.Parse("7.6.5.4");
        private SatelliteStatus TestSatelliteStatus = SatelliteStatus.Good;

        private SatelliteSummaryMapper TestObject = new SatelliteSummaryMapper();

        [TestInitialize]
        public void Init()
        {
            TestSatellite.ID = TestSatelliteID;
            TestSatellite.Name = TestSatelliteName;
            TestSatellite.IPAddress = TestSatelliteIP;
            TestSatellite.Status = TestSatelliteStatus;
        }

        [TestMethod]
        public void MapsID()
        {
            SatelliteSummaryViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteID, viewModel.ID);
        }

        [TestMethod]
        public void MapsName()
        {
            SatelliteSummaryViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteName, viewModel.Name);
        }

        [TestMethod]
        public void MapsIPAddress()
        {
            SatelliteSummaryViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteIP.ToString(), viewModel.IPAddress);
        }

        [TestMethod]
        public void MapsStatus()
        {
            SatelliteSummaryViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteStatus, viewModel.Status);
        }
    }
}
