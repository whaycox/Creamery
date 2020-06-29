using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class ProjectEntityQueryTest : BaseSqlQueryTemplate
    {
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();
        private TestEntity TestEntity = new TestEntity();

        private Mock<ISqlTable> MockProjectedTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private ProjectEntityQuery<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestColumns.Add(MockColumn.Object);

            MockProjectedTable
                .Setup(table => table.Columns)
                .Returns(TestColumns);
            MockProjectedTable
                .Setup(table => table.ProjectEntity(It.IsAny<ISqlQueryReader>()))
                .Returns(TestEntity);

            TestObject = new ProjectEntityQuery<ITestDataModel, TestEntity>(
                MockQueryContext.Object,
                MockPhraseBuilder.Object,
                MockProjectedTable.Object,
                MockSource.Object);
        }

        [TestMethod]
        public void GenerateCommandBuildsSelectColumnsPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.SelectColumnsToken(TestColumns), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsFromUniversePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.FromUniverseTokens(MockSource.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public void GeneratedTokensAreExpected(int sourceTokensGenerated)
        {
            List<ISqlQueryToken> expectedTokens = new List<ISqlQueryToken>();
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.SelectColumnsToken(It.IsAny<IEnumerable<ISqlColumn>>())));
            expectedTokens.AddRange(SetupPhraseBuilder(builder => builder.FromUniverseTokens(It.IsAny<ISqlUniverse>()), sourceTokensGenerated));

            TestObject.GenerateCommand();

            CollectionAssert.AreEqual(expectedTokens, FormattedTokens);
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

            MockProjectedTable.Verify(table => table.ProjectEntity(MockQueryReader.Object), Times.Exactly(entities));
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
