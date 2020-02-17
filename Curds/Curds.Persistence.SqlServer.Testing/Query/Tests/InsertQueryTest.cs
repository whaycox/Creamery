using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Domain;
    using Model.Domain;
    using Model.Abstraction;

    [TestClass]
    public class InsertQueryTest
    {
        private Table TestTable = new Table();
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity<TestEntity> TestValueEntity = new ValueEntity<TestEntity>();

        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();

        private InsertQuery<TestEntity> TestObject = new InsertQuery<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            TestValueEntity.Source = TestEntity;

            TestObject.AssignIdentityDelegate = MockAssignIdentityDelegate.Object;

            TestObject.Table = TestTable;
            TestObject.Entities.Add(TestValueEntity);
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            int callOrder = 0;
            MockQueryWriter
                .Setup(writer => writer.CreateTemporaryIdentityTable(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 0));
            MockQueryWriter
                .Setup(writer => writer.Insert(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 1));
            MockQueryWriter
                .Setup(writer => writer.OutputIdentitiesToTemporaryTable(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 2));
            MockQueryWriter
                .Setup(writer => writer.ValueEntities(It.IsAny<IEnumerable<ValueEntity>>()))
                .Callback(() => Assert.AreEqual(callOrder++, 3));
            MockQueryWriter
                .Setup(writer => writer.SelectTemporaryIdentityTable(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 4));
            MockQueryWriter
                .Setup(writer => writer.DropTemporaryIdentityTable(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 5));

            TestObject.Write(MockQueryWriter.Object);

            MockQueryWriter.Verify(writer => writer.CreateTemporaryIdentityTable(TestTable), Times.Once);
            MockQueryWriter.Verify(writer => writer.Insert(TestTable), Times.Once);
            MockQueryWriter.Verify(writer => writer.OutputIdentitiesToTemporaryTable(TestTable), Times.Once);
            MockQueryWriter.Verify(writer => writer.ValueEntities(It.Is<IEnumerable<ValueEntity>>(arg => arg.Count() == 1 && arg.First() == TestValueEntity)), Times.Once);
            MockQueryWriter.Verify(writer => writer.SelectTemporaryIdentityTable(TestTable), Times.Once);
            MockQueryWriter.Verify(writer => writer.DropTemporaryIdentityTable(TestTable), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public async Task ProcessResultsAssignsIdentitiesForEachEntities(int entities)
        {
            TestObject.Entities.Clear();
            var sequenceSetup = MockQueryReader.SetupSequence(reader => reader.Advance());
            for (int i = 0; i < entities; i++)
            {
                TestObject.Entities.Add(TestValueEntity);
                sequenceSetup.ReturnsAsync(true);
            }
            sequenceSetup.ReturnsAsync(false);

            await TestObject.ProcessResult(MockQueryReader.Object);

            MockAssignIdentityDelegate.Verify(del => del(MockQueryReader.Object, TestEntity), Times.Exactly(entities));
        }
    }
}
