using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class ColumnNameSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private string TestAlias = nameof(TestAlias);
        private string TestName = nameof(TestName);

        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private ColumnNameSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            SetupTokenFactoryForMockToken(factory => factory.QualifiedObject(It.IsAny<string[]>()));
            MockColumn
                .Setup(column => column.Table.Alias)
                .Returns(TestAlias);
            MockColumn
                .Setup(column => column.Name)
                .Returns(TestName);

            TestObject = new ColumnNameSqlQueryToken(
                MockTokenFactory.Object,
                MockColumn.Object);
        }

        [TestMethod]
        public void WithAliasGeneratesExpectedTokens()
        {
            TestObject.UseAlias = true;

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObject(TestAlias, TestName), Times.Once);
        }

        [TestMethod]
        public void WithoutAliasGeneratesExpectedTokens()
        {
            TestObject.UseAlias = false;

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObject(TestName), Times.Once);
        }

        [TestMethod]
        public void GeneratedTokenAcceptsVisitor()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
