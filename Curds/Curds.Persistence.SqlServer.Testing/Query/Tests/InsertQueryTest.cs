using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class InsertQueryTest
    {
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();

        private InsertQuery<TestEntity> TestObject = new InsertQuery<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            MockEntityModel
                .Setup(model => model.ValueEntity)
                .Returns(MockValueEntityDelegate.Object);
            MockValueEntityDelegate
                .Setup(del => del(It.IsAny<IEntity>()))
                .Returns(TestValueEntity);
            MockEntityModel
                .Setup(model => model.AssignIdentity)
                .Returns(MockAssignIdentityDelegate.Object);

            TestObject.Model = MockEntityModel.Object;
            TestObject.Entities.Add(TestEntity);
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            int callOrder = 0;
            MockQueryWriter
                .Setup(writer => writer.CreateTemporaryIdentityTable(It.IsAny<IEntityModel>()))
                .Callback(() => Assert.AreEqual(callOrder++, 0));
            MockQueryWriter
                .Setup(writer => writer.Insert(It.IsAny<IEntityModel>()))
                .Callback(() => Assert.AreEqual(callOrder++, 1));
            MockQueryWriter
                .Setup(writer => writer.OutputIdentitiesToTemporaryTable(It.IsAny<IEntityModel>()))
                .Callback(() => Assert.AreEqual(callOrder++, 2));
            MockQueryWriter
                .Setup(writer => writer.ValueEntities(It.IsAny<IEnumerable<ValueEntity>>()))
                .Callback(() => Assert.AreEqual(callOrder++, 3));
            MockQueryWriter
                .Setup(writer => writer.SelectTemporaryIdentityTable(It.IsAny<IEntityModel>()))
                .Callback(() => Assert.AreEqual(callOrder++, 4));
            MockQueryWriter
                .Setup(writer => writer.DropTemporaryIdentityTable(It.IsAny<IEntityModel>()))
                .Callback(() => Assert.AreEqual(callOrder++, 5));

            TestObject.Write(MockQueryWriter.Object);

            MockQueryWriter.Verify(writer => writer.CreateTemporaryIdentityTable(MockEntityModel.Object), Times.Once);
            MockQueryWriter.Verify(writer => writer.Insert(MockEntityModel.Object), Times.Once);
            MockQueryWriter.Verify(writer => writer.OutputIdentitiesToTemporaryTable(MockEntityModel.Object), Times.Once);
            MockQueryWriter.Verify(writer => writer.ValueEntities(It.Is<IEnumerable<ValueEntity>>(arg => arg.Count() == 1)), Times.Once);
            MockQueryWriter.Verify(writer => writer.SelectTemporaryIdentityTable(MockEntityModel.Object), Times.Once);
            MockQueryWriter.Verify(writer => writer.DropTemporaryIdentityTable(MockEntityModel.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public void WriteBuildsValueEntityFromDelegateForEachEntity(int entities)
        {
            TestObject.Entities.Clear();
            for (int i = 0; i < entities; i++)
                TestObject.Entities.Add(TestEntity);

            TestObject.Write(MockQueryWriter.Object);

            MockValueEntityDelegate.Verify(del => del(TestEntity), Times.Exactly(entities));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public async Task ProcessResultsAssignsIdentitiesForEachEntity(int entities)
        {
            TestObject.Entities.Clear();
            var sequenceSetup = MockQueryReader.SetupSequence(reader => reader.Advance());
            for (int i = 0; i < entities; i++)
            {
                TestObject.Entities.Add(TestEntity);
                sequenceSetup.ReturnsAsync(true);
            }
            sequenceSetup.ReturnsAsync(false);

            await TestObject.ProcessResult(MockQueryReader.Object);

            MockAssignIdentityDelegate.Verify(del => del(MockQueryReader.Object, TestEntity), Times.Exactly(entities));
        }
    }
}
