using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class InsertQueryTest
    {
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private InsertQuery<ITestDataModel, TestEntity> TestObject = null;//new InsertQuery<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
            //MockTable
            //    .Setup(model => model.BuildValueEntity(It.IsAny<IEntity>()))
            //    .Returns(TestValueEntity);

            //TestObject.Table = MockTable.Object;
            //TestObject.Entities.Add(TestEntity);
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            throw new NotImplementedException();
            //int callOrder = 0;
            //MockQueryWriter
            //    .Setup(writer => writer.CreateTemporaryIdentityTable(It.IsAny<ISqlTable>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 0));
            //MockQueryWriter
            //    .Setup(writer => writer.Insert(It.IsAny<ISqlTable>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 1));
            //MockQueryWriter
            //    .Setup(writer => writer.OutputIdentitiesToTemporaryTable(It.IsAny<ISqlTable>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 2));
            //MockQueryWriter
            //    .Setup(writer => writer.ValueEntities(It.IsAny<IEnumerable<ValueEntity>>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 3));
            //MockQueryWriter
            //    .Setup(writer => writer.SelectTemporaryIdentityTable(It.IsAny<ISqlTable>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 4));
            //MockQueryWriter
            //    .Setup(writer => writer.DropTemporaryIdentityTable(It.IsAny<ISqlTable>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 5));

            //TestObject.Write(MockQueryWriter.Object);

            //MockQueryWriter.Verify(writer => writer.CreateTemporaryIdentityTable(MockTable.Object), Times.Once);
            //MockQueryWriter.Verify(writer => writer.Insert(MockTable.Object), Times.Once);
            //MockQueryWriter.Verify(writer => writer.OutputIdentitiesToTemporaryTable(MockTable.Object), Times.Once);
            //MockQueryWriter.Verify(writer => writer.ValueEntities(It.Is<IEnumerable<ValueEntity>>(arg => arg.Count() == 1)), Times.Once);
            //MockQueryWriter.Verify(writer => writer.SelectTemporaryIdentityTable(MockTable.Object), Times.Once);
            //MockQueryWriter.Verify(writer => writer.DropTemporaryIdentityTable(MockTable.Object), Times.Once);
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
            throw new NotImplementedException();
            //TestObject.Entities.Clear();
            //for (int i = 0; i < entities; i++)
            //    TestObject.Entities.Add(TestEntity);

            //TestObject.Write(MockQueryWriter.Object);

            //MockTable.Verify(table => table.BuildValueEntity(TestEntity), Times.Exactly(entities));
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

            MockTable.Verify(table => table.AssignIdentities(MockQueryReader.Object, TestEntity), Times.Exactly(entities));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task LessIdentitiesThanEntitiesThrows()
        {
            MockQueryReader
                .Setup(reader => reader.Advance())
                .ReturnsAsync(false);

            await TestObject.ProcessResult(MockQueryReader.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task MoreIdentitiesThanEntitiesThrows()
        {
            MockQueryReader
                .SetupSequence(reader => reader.Advance())
                .ReturnsAsync(true)
                .ReturnsAsync(true);

            await TestObject.ProcessResult(MockQueryReader.Object);
        }
    }
}
