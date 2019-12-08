using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Application.ViewModels.Satellite.Tests
{
    using Domain;
    using Implementation;
    using Template;

    [TestClass]
    public class SatelliteMapperTest : SatelliteMapperTemplate
    {
        private SatelliteMapper TestObject = new SatelliteMapper();

        [TestMethod]
        public void MapsID()
        {
            SatelliteViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteID, viewModel.ID);
        }

        [TestMethod]
        public void MapsName()
        {
            SatelliteViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteName, viewModel.Name);
        }

        [TestMethod]
        public void MapsIPAddress()
        {
            SatelliteViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.AreEqual(TestSatelliteIP.ToString(), viewModel.IPField.Value);
        }

        [TestMethod]
        public void MapsStatus()
        {
            SatelliteViewModel viewModel = TestObject.Map(TestSatellite);

            Assert.IsInstanceOfType(viewModel.StatusField.ViewModel, typeof(SatelliteStatusViewModel));
            SatelliteStatusViewModel statusModel = (SatelliteStatusViewModel)viewModel.StatusField.ViewModel;
            Assert.AreEqual(TestSatelliteStatus, statusModel.Status);
        }
    }
}
