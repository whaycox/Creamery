using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Whey;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class DeleteEntityQueryTest : BaseSqlQueryTemplate
    {
        private Mock<ISqlTable> MockDeletedTable = new Mock<ISqlTable>();

        private DeleteEntityQuery<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new DeleteEntityQuery<ITestDataModel, TestEntity>(
                MockQueryContext.Object,
                MockDeletedTable.Object,
                MockSource.Object);
        }

        [TestMethod]
        public void GenerateCommandBuildsDeleteTablePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.DeleteTableToken(MockDeletedTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandGetsTokensFromSource()
        {
            TestObject.GenerateCommand();

            MockSource.Verify(source => source.Tokens, Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(15)]
        public void GeneratedTokensAreExpected(int sourceTokensGenerated)
        {
            List<ISqlQueryToken> expectedTokens = new List<ISqlQueryToken>();
            expectedTokens.Add(MockPhraseBuilder.SetupMock(builder => builder.DeleteTableToken(It.IsAny<ISqlTable>())));
            expectedTokens.AddRange(MockSource.SetupMock(source => source.Tokens, sourceTokensGenerated));

            TestObject.GenerateCommand();

            CollectionAssert.AreEqual(expectedTokens, FormattedTokens);
        }
    }
}
