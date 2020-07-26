using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Whey;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class ColumnDefinitionSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private ColumnDefinitionSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            SetupTokenFactory(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));

            TestObject = new ColumnDefinitionSqlQueryToken(
                MockTokenFactory.Object,
                MockColumn.Object);
        }

        [TestMethod]
        public void AcceptVisitorBuildsColumnNameToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.ColumnName(MockColumn.Object, false), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorBuildsDbTypeToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.DbType(MockColumn.Object), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorBuildsPhraseToken()
        {
            ISqlQueryToken testColumnNameToken = MockTokenFactory.SetupMock(factory => factory.ColumnName(It.IsAny<ISqlColumn>(), It.IsAny<bool>()));
            ISqlQueryToken testDbTypeToken = MockTokenFactory.SetupMock(factory => factory.DbType(It.IsAny<ISqlColumn>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(testColumnNameToken, testDbTypeToken), Times.Once);
        }

        [TestMethod]
        public void AcceptVisitorPassesToPhraseToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
