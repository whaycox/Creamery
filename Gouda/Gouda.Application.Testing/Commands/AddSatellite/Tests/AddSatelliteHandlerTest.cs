using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.AddSatellite.Tests
{
    using Domain;
    using Gouda.Domain;
    using Implementation;
    using Enumerations;
    using Persistence.Abstraction;

    [TestClass]
    public class AddSatelliteHandlerTest
    {
        private int TestSatelliteID = 14;
        private string TestSatelliteName = nameof(TestSatelliteName);
        private IPAddress TestSatelliteIP = IPAddress.Parse("10.10.10.10");
        private AddSatelliteCommand TestCommand = new AddSatelliteCommand();

        private Satellite InsertedSatellite = null;

        private Mock<IGoudaDatabase> MockDatabase = new Mock<IGoudaDatabase>();

        private AddSatelliteHandler TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestCommand.SatelliteName = TestSatelliteName;
            TestCommand.SatelliteIP = TestSatelliteIP.ToString();

            MockDatabase
                .Setup(db => db.Satellite.Insert(It.IsAny<Satellite>()))
                .Callback<Satellite>(satellite => { satellite.ID = TestSatelliteID; InsertedSatellite = satellite; });

            TestObject = new AddSatelliteHandler(MockDatabase.Object);
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
        public async Task ReturnsSummarizedViewModel()
        {
            AddSatelliteResult result = await TestObject.Handle(TestCommand, default);

            Assert.AreEqual(TestSatelliteID, result.NewSatellite.ID);
            Assert.AreEqual(TestSatelliteName, result.NewSatellite.Name);
            Assert.AreEqual(TestSatelliteIP.ToString(), result.NewSatellite.IPAddress);
            Assert.AreEqual(default, result.NewSatellite.Status);
        }
    }
}
