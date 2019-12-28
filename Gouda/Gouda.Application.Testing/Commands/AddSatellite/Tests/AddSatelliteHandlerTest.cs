using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.AddSatellite.Tests
{
    using Gouda.Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Template;
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Domain;

    [TestClass]
    public class AddSatelliteHandlerTest : AddSatelliteTemplate
    {
        private int TestSatelliteID = 14;
        private SatelliteSummaryViewModel TestSummaryViewModel = new SatelliteSummaryViewModel();

        private Satellite InsertedSatellite = null;

        private Mock<IGoudaDatabase> MockDatabase = new Mock<IGoudaDatabase>();
        private Mock<ISatelliteSummaryMapper> MockSatelliteSummaryMapper = new Mock<ISatelliteSummaryMapper>();

        private AddSatelliteHandler TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockDatabase
                .Setup(db => db.Satellite.Insert(It.IsAny<Satellite>()))
                .Callback<Satellite>(satellite => { satellite.ID = TestSatelliteID; InsertedSatellite = satellite; });
            MockSatelliteSummaryMapper
                .Setup(mapper => mapper.Map(It.IsAny<Satellite>()))
                .Returns(TestSummaryViewModel);

            TestObject = new AddSatelliteHandler(MockDatabase.Object, MockSatelliteSummaryMapper.Object);
        }

        [TestMethod]
        public async Task AddsSatelliteToDatabase()
        {
            await TestObject.Handle(TestCommand, default);

            MockDatabase.Verify(db => db.Satellite.Insert(It.IsAny<Satellite>()), Times.Once);
            Assert.AreEqual(TestSatelliteName, InsertedSatellite.Name);
            Assert.AreEqual(TestSatelliteIP, InsertedSatellite.IPAddress);
        }

        [TestMethod]
        public async Task SavesChangesInDatabase()
        {
            await TestObject.Handle(TestCommand, default);

            MockDatabase.Verify(db => db.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task MapsAddedSatelliteToViewModel()
        {
            await TestObject.Handle(TestCommand, default);

            MockSatelliteSummaryMapper.Verify(mapper => mapper.Map(InsertedSatellite), Times.Once);
        }

        [TestMethod]
        public async Task ReturnsSummarizedViewModel()
        {
            SatelliteSummaryViewModel result = await TestObject.Handle(TestCommand, default);

            Assert.AreSame(TestSummaryViewModel, result);
        }
    }
}
