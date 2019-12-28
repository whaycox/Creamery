using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.AddCheck.Tests
{
    using Implementation;
    using Domain;
    using Template;
    using Gouda.Domain;
    using Persistence.Abstraction;
    using ViewModels.Satellite.Abstraction;

    [TestClass]
    public class AddCheckHandlerTest : AddCheckTemplate
    {
        private Mock<IGoudaDatabase> MockGoudaDatabase = new Mock<IGoudaDatabase>();
        private Mock<ICheckDefinitionMapper> MockViewModelMapper = new Mock<ICheckDefinitionMapper>();

        private AddCheckHandler TestObject = null;

        private CheckDefinition AddedCheckDefinition = null;

        [TestInitialize]
        public void Init()
        {
            MockGoudaDatabase
                .Setup(db => db.CheckDefinition.Insert(It.IsAny<CheckDefinition>()))
                .Callback<CheckDefinition>(check => AddedCheckDefinition = check);

            TestObject = new AddCheckHandler(
                MockGoudaDatabase.Object,
                MockViewModelMapper.Object);
        }

        [TestMethod]
        public async Task AddsToDatabase()
        {
            await TestObject.Handle(TestCommand, default);

            MockGoudaDatabase.Verify(db => db.CheckDefinition.Insert(It.IsAny<CheckDefinition>()), Times.Once);
        }

        [TestMethod]
        public async Task AddedCheckDefinitonIsExpected()
        {
            await TestObject.Handle(TestCommand, default);

            Assert.AreEqual(TestName, AddedCheckDefinition.Name);
            Assert.AreEqual(TestCheckID, AddedCheckDefinition.CheckID);
            Assert.AreEqual(TestSatelliteID, AddedCheckDefinition.SatelliteID);
        }
    }
}
