using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Whey;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class TableDefinitionSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
        private Mock<ISqlQueryToken> MockPhraseToken = new Mock<ISqlQueryToken>();

        private TableDefinitionSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestColumns.Add(MockColumn.Object);

            MockTokenFactory
                .Setup(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()))
                .Returns(MockPhraseToken.Object);
            MockTable
                .Setup(table => table.Columns)
                .Returns(TestColumns);

            TestObject = new TableDefinitionSqlQueryToken(
                MockTokenFactory.Object,
                MockTable.Object);
        }

        [TestMethod]
        public void AcceptVisitorBuildsTableNameToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.TableName(MockTable.Object, false, true), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorBuildsColumnListToken()
        {
            throw new System.NotImplementedException();
            //TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            //MockTokenFactory.Verify(factory => factory.ColumnList(TestColumns, true), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorBuildsPhraseToken()
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken testTableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(It.IsAny<ISqlTable>(), It.IsAny<bool>(), It.IsAny<bool>()));
            //ISqlQueryToken testColumnListToken = MockTokenFactory.SetupMock(factory => factory.ColumnList(It.IsAny<IEnumerable<ISqlColumn>>(), It.IsAny<bool>()));

            //TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            //MockTokenFactory.Verify(factory => factory.Phrase(testTableNameToken, testColumnListToken), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorPassesToPhraseToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockPhraseToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
