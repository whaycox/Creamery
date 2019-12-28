using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.Queries.DisplaySatellite.Tests
{
    using Gouda.Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Template;
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Domain;

    [TestClass]
    public class DisplaySatelliteHandlerTest : DisplaySatelliteTemplate
    {
        private Satellite TestSatellite = new Satellite();
        private SatelliteViewModel TestViewModel = new SatelliteViewModel();

        private Mock<IGoudaDatabase> MockGoudaDatabase = new Mock<IGoudaDatabase>();
        private Mock<ISatelliteMapper> MockSatelliteMapper = new Mock<ISatelliteMapper>();

        private DisplaySatelliteHandler TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockGoudaDatabase
                .Setup(db => db.Satellite.Fetch(It.IsAny<int>()))
                .ReturnsAsync(TestSatellite);
            MockSatelliteMapper
                .Setup(mapper => mapper.Map(It.IsAny<Satellite>()))
                .Returns(TestViewModel);

            TestObject = new DisplaySatelliteHandler(MockGoudaDatabase.Object, MockSatelliteMapper.Object);
        }

        [TestMethod]
        public async Task RetrievesSatelliteByID()
        {
            await TestObject.Handle(TestQuery, default);

            MockGoudaDatabase.Verify(db => db.Satellite.Fetch(TestSatelliteID), Times.Once);
        }

        [TestMethod]
        public async Task MapsRetrievedSatelliteToViewModel()
        {
            await TestObject.Handle(TestQuery, default);

            MockSatelliteMapper.Verify(mapper => mapper.Map(TestSatellite), Times.Once);
        }

        [TestMethod]
        public async Task ReturnsMappedViewModel()
        {
            SatelliteViewModel viewModel = await TestObject.Handle(TestQuery, default);

            Assert.AreSame(TestViewModel, viewModel);
        }
    }
}
