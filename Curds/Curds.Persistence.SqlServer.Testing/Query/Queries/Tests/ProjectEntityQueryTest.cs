﻿using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Model.Domain;
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;

    [TestClass]
    public class ProjectEntityQueryTest
    {
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();
        private TestEntity TestEntity = new TestEntity();

        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
        private Mock<ISqlUniverse<TestEntity>> MockUniverse = new Mock<ISqlUniverse<TestEntity>>();

        private ProjectEntityQuery<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestColumns.Add(MockColumn.Object);

            MockTable
                .Setup(table => table.Columns)
                .Returns(TestColumns);
            MockTable
                .Setup(table => table.ProjectEntity(It.IsAny<ISqlQueryReader>()))
                .Returns(TestEntity);

            TestObject.ProjectedTable = MockTable.Object;
            TestObject.Source = MockUniverse.Object;
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            throw new NotImplementedException();
            //int callOrder = 0;
            //MockQueryWriter
            //    .Setup(writer => writer.Select(It.IsAny<List<ISqlColumn>>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 0));
            //MockQueryWriter
            //    .Setup(writer => writer.From(It.IsAny<ISqlUniverse>()))
            //    .Callback(() => Assert.AreEqual(callOrder++, 1));

            //TestObject.Write(MockQueryWriter.Object);

            //MockQueryWriter.Verify(writer => writer.Select(TestColumns), Times.Once);
            //MockQueryWriter.Verify(writer => writer.From(MockUniverse.Object), Times.Once);
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

            MockTable.Verify(table => table.ProjectEntity(MockQueryReader.Object), Times.Exactly(entities));
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