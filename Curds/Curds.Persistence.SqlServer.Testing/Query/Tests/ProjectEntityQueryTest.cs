using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Model.Domain;
    using Abstraction;
    using Model.Abstraction;

    [TestClass]
    public class ProjectEntityQueryTest
    {
        private List<IValueModel> TestValueModels = new List<IValueModel>();
        private TestEntity TestEntity = new TestEntity();

        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();
        private Mock<ProjectEntityDelegate> MockProjectEntityDelegate = new Mock<ProjectEntityDelegate>();

        private ProjectEntityQuery<TestEntity> TestObject = new ProjectEntityQuery<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            TestValueModels.Add(MockValueModel.Object);

            MockEntityModel
                .Setup(model => model.Values)
                .Returns(TestValueModels);
            MockEntityModel
                .Setup(model => model.ProjectEntity)
                .Returns(MockProjectEntityDelegate.Object);
            MockProjectEntityDelegate
                .Setup(del => del(It.IsAny<ISqlQueryReader>()))
                .Returns(TestEntity);

            TestObject.Model = MockEntityModel.Object;
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            int callOrder = 0;
            MockQueryWriter
                .Setup(writer => writer.Select(It.IsAny<List<IValueModel>>()))
                .Callback(() => Assert.AreEqual(callOrder++, 0));
            MockQueryWriter
                .Setup(writer => writer.From(It.IsAny<IEntityModel>()))
                .Callback(() => Assert.AreEqual(callOrder++, 1));

            TestObject.Write(MockQueryWriter.Object);

            MockQueryWriter.Verify(writer => writer.Select(TestValueModels), Times.Once);
            MockQueryWriter.Verify(writer => writer.From(MockEntityModel.Object), Times.Once);
        }

        private void SetupReaderForNEntities(int entities)
        {
            var sequenceSetup = MockQueryReader.SetupSequence(reader => reader.Advance());
            for (int i = 0; i < entities; i++)
                sequenceSetup.ReturnsAsync(true);
            sequenceSetup.ReturnsAsync(false);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public async Task ProcessResultsProjectsEachReturnedEntity(int entities)
        {
            SetupReaderForNEntities(entities);

            await TestObject.ProcessResult(MockQueryReader.Object);

            MockProjectEntityDelegate.Verify(del => del(MockQueryReader.Object), Times.Exactly(entities));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public async Task ProcessResultsAddsEachProjectionToResults(int entities)
        {
            SetupReaderForNEntities(entities);

            await TestObject.ProcessResult(MockQueryReader.Object);

            Assert.AreEqual(entities, TestObject.Results.Count);
            foreach (TestEntity entity in TestObject.Results)
                Assert.AreSame(TestEntity, entity);
        }
    }
}
