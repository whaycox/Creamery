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
    using Template;

    [TestClass]
    public class SatelliteSummaryMapperTest : SatelliteMapperTemplate
    {
        private SatelliteSummaryMapper TestObject = new SatelliteSummaryMapper();

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

            Assert.AreEqual(TestSatelliteStatus, viewModel.StatusViewModel.Status);
        }
    }
}
