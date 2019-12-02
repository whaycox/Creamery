using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.Application.Queries.ListSatellites.Tests
{
    using Domain;
    using Gouda.Domain;
    using Implementation;
    using Persistence.Abstraction;
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Domain;

    [TestClass]
    public class ListSatellitesHandlerTest
    {
        private ListSatellitesQuery TestQuery = new ListSatellitesQuery();
        private List<Satellite> TestSatellites = new List<Satellite>();
        private Satellite TestSatellite = new Satellite();
        private SatelliteSummaryViewModel TestSummaryViewModel = new SatelliteSummaryViewModel();

        private Mock<IGoudaDatabase> MockGoudaDatabase = new Mock<IGoudaDatabase>();
        private Mock<ISatelliteSummaryMapper> MockSatelliteSummaryMapper = new Mock<ISatelliteSummaryMapper>();

        private ListSatellitesHandler TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockGoudaDatabase
                .Setup(db => db.Satellite.FetchAll())
                .ReturnsAsync(TestSatellites);
            MockSatelliteSummaryMapper
                .Setup(mapper => mapper.Map(It.IsAny<Satellite>()))
                .Returns(TestSummaryViewModel);

            TestObject = new ListSatellitesHandler(MockGoudaDatabase.Object, MockSatelliteSummaryMapper.Object);
        }

        [TestMethod]
        public async Task RetrievesAllSatellites()
        {
            await TestObject.Handle(TestQuery, default);

            MockGoudaDatabase.Verify(db => db.Satellite.FetchAll(), Times.Once);
        }

        private void SetupNSatellites(int satellites)
        {
            for (int i = 0; i < satellites; i++)
                TestSatellites.Add(TestSatellite);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(13)]
        public async Task MapsEachReturnedSatelliteToViewModel(int satellites)
        {
            SetupNSatellites(satellites);

            await TestObject.Handle(TestQuery, default);

            MockSatelliteSummaryMapper.Verify(mapper => mapper.Map(TestSatellite), Times.Exactly(satellites));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(13)]
        public async Task ReturnsAllSatellitesInResult(int satellites)
        {
            SetupNSatellites(satellites);

            ListSatellitesResult result = await TestObject.Handle(TestQuery, default);

            Assert.AreEqual(satellites, result.Satellites.Count);
            for (int i = 0; i < satellites; i++)
                Assert.AreSame(TestSummaryViewModel, result.Satellites[i]);
        }
    }
}
