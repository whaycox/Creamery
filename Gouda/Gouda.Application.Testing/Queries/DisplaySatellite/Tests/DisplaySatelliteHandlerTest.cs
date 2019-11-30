using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.Queries.DisplaySatellite.Tests
{
    using Implementation;
    using Domain;
    using Persistence.Abstraction;
    using Gouda.Domain;
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Domain;

    [TestClass]
    public class DisplaySatelliteHandlerTest
    {
        private DisplaySatelliteQuery TestQuery = new DisplaySatelliteQuery();
        private int TestSatelliteID = 5;
        private Satellite TestSatellite = new Satellite();
        private SatelliteViewModel TestViewModel = new SatelliteViewModel();

        private Mock<IGoudaDatabase> MockGoudaDatabase = new Mock<IGoudaDatabase>();
        private Mock<ISatelliteMapper> MockSatelliteMapper = new Mock<ISatelliteMapper>();

        private DisplaySatelliteHandler TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestQuery.SatelliteID = TestSatelliteID;

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
        public async Task ReturnsMappedViewModelInResult()
        {
            DisplaySatelliteResult result = await TestObject.Handle(TestQuery, default);

            Assert.AreSame(TestViewModel, result.Satellite);
        }
    }
}
