using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Whey;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class TableNameSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private string TestSchema = nameof(TestSchema);
        private string TestName = nameof(TestName);
        private string TestAlias = nameof(TestAlias);

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private TableNameSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockTable
                .Setup(table => table.Schema)
                .Returns(TestSchema);
            MockTable
                .Setup(table => table.Name)
                .Returns(TestName);
            MockTable
                .Setup(table => table.Alias)
                .Returns(TestAlias);

            TestObject = new TableNameSqlQueryToken(
                MockTokenFactory.Object,
                MockTable.Object);
        }

        [TestMethod]
        public void AcceptFormatVisitorWithoutAliasGeneratesExpectedToken()
        {
            TestObject.UseAlias = false;
            SetupTokenFactoryForMockToken(factory => factory.QualifiedObject(It.IsAny<string[]>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObject(
                TestSchema,
                TestName), Times.Once);
            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorWithAliasWithoutSqlNameGeneratesExpectedToken()
        {
            TestObject.UseAlias = true;
            TestObject.UseSqlName = false;
            SetupTokenFactoryForMockToken(factory => factory.ObjectName(It.IsAny<string>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.ObjectName(TestAlias), Times.Once);
            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorWithAliasAndSqlNameGeneratesExpectedToken()
        {
            TestObject.UseAlias = true;
            TestObject.UseSqlName = true;
            ISqlQueryToken sqlNameToken = MockTokenFactory.SetupMock(factory => factory.QualifiedObject(It.IsAny<string[]>()));
            ISqlQueryToken aliasToken = MockTokenFactory.SetupMock(factory => factory.ObjectName(It.IsAny<string>()));
            SetupTokenFactoryForMockToken(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(
                sqlNameToken,
                aliasToken), Times.Once);
            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
