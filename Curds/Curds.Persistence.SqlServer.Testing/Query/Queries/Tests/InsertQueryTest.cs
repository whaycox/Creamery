using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whey;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class InsertQueryTest : BaseSqlQueryTemplate
    {
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockIdentityColumn = new Mock<ISqlColumn>();
        private Mock<ISqlTable> MockInsertedIdentityTable = new Mock<ISqlTable>();

        private InsertQuery<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockQueryContext
                .Setup(context => context.AddTable<TestEntity>())
                .Returns(MockTable.Object);
            MockTable
                .Setup(table => table.Identity)
                .Returns(MockIdentityColumn.Object);
            MockTable
                .Setup(table => table.InsertedIdentityTable)
                .Returns(MockInsertedIdentityTable.Object);
            MockTable
                .Setup(model => model.BuildValueEntity(It.IsAny<IEntity>()))
                .Returns(TestValueEntity);

            TestObject = new InsertQuery<ITestDataModel, TestEntity>(MockQueryContext.Object);
            TestObject.Entities.Add(TestEntity);
        }

        [TestMethod]
        public void GenerateCommandGetsInsertedIdentityTable()
        {
            TestObject.GenerateCommand();

            MockTable.Verify(table => table.InsertedIdentityTable, Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsCreateTemporaryIdentityPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.CreateTableToken(MockInsertedIdentityTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsInsertToTablePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.InsertToTableToken(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsOutputToTemporaryIdentityPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.OutputToTemporaryIdentityToken(MockTable.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public void GenerateCommandBuildsValueEntityFromTableForEachEntity(int entities)
        {
            TestObject.Entities.Clear();
            for (int i = 0; i < entities; i++)
                TestObject.Entities.Add(TestEntity);

            TestObject.GenerateCommand();

            MockTable.Verify(table => table.BuildValueEntity(TestEntity), Times.Exactly(entities));
        }

        [TestMethod]
        public void GenerateCommandBuildsValueEntitiesPhrase()
        {
            List<ValueEntity> suppliedValueEntities = null;
            MockPhraseBuilder
                .Setup(builder => builder.ValueEntitiesToken(It.IsAny<ISqlQueryParameterBuilder>(), It.IsAny<IEnumerable<ValueEntity>>()))
                .Callback<ISqlQueryParameterBuilder, IEnumerable<ValueEntity>>((builder, entities) => suppliedValueEntities = entities.ToList());

            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.ValueEntitiesToken(MockParameterBuilder.Object, It.IsAny<IEnumerable<ValueEntity>>()), Times.Once);
            CollectionAssert.AreEqual(new[] { TestValueEntity }, suppliedValueEntities);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void GenerateCommandBuildsSelectColumnsFromTemporaryIdentityTablePhrase(int columns)
        {
            List<ISqlColumn> testColumns = MockInsertedIdentityTable.SetupMock(table => table.Columns, columns);

            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.SelectColumnsToken(testColumns), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsFromTemporaryIdentityTablePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.FromTableToken(MockInsertedIdentityTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsDropTemporaryIdentityTablePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.DropTableToken(MockInsertedIdentityTable.Object), Times.Once);
        }

        [TestMethod]
        public void GeneratedTokensAreExpected()
        {
            List<ISqlQueryToken> expectedTokens = new List<ISqlQueryToken>();
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.CreateTableToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.InsertToTableToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.OutputToTemporaryIdentityToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.ValueEntitiesToken(It.IsAny<ISqlQueryParameterBuilder>(), It.IsAny<IEnumerable<ValueEntity>>())));
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.SelectColumnsToken(It.IsAny<IEnumerable<ISqlColumn>>())));
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.FromTableToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.DropTableToken(It.IsAny<ISqlTable>())));

            TestObject.GenerateCommand();

            CollectionAssert.AreEqual(expectedTokens, FormattedTokens);
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
