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

        private TableDefinitionSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestColumns.Add(MockColumn.Object);

            SetupTokenFactoryForMockToken(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));
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

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(7)]
        [DataRow(13)]
        [DataRow(15)]
        public void AcceptVisitorBuildsColumnDefinitionTokenForEachColumn(int columnsInTable)
        {
            TestColumns.Clear();
            for (int i = 0; i < columnsInTable; i++)
                TestColumns.Add(MockColumn.Object);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.ColumnDefinition(MockColumn.Object), Times.Exactly(columnsInTable));
        }

        [TestMethod]
        public void AcceptFormatVisitorBuildsGroupedListOfColumnDefinitions()
        {
            ISqlQueryToken columnDefinitionToken = MockTokenFactory.SetupMock(factory => factory.ColumnDefinition(MockColumn.Object));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.GroupedList(new[] { columnDefinitionToken }, true), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorBuildsPhraseToken()
        {
            ISqlQueryToken testTableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(It.IsAny<ISqlTable>(), It.IsAny<bool>(), It.IsAny<bool>()));
            ISqlQueryToken testGroupedListToken = MockTokenFactory.SetupMock(factory => factory.GroupedList(It.IsAny<IEnumerable<ISqlQueryToken>>(), It.IsAny<bool>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(testTableNameToken, testGroupedListToken), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorPassesToPhraseToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
