using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Query.Domain;
    using Template;

    [TestClass]
    public class InsertedIdentityColumnSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private string TestIdentityName = nameof(TestIdentityName);

        private Mock<ISqlColumn> MockIdentity = new Mock<ISqlColumn>();

        private InsertedIdentityColumnSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            SetupTokenFactoryForMockToken(factory => factory.QualifiedObject(It.IsAny<string[]>()));
            MockIdentity
                .Setup(column => column.Name)
                .Returns(TestIdentityName);

            TestObject = new InsertedIdentityColumnSqlQueryToken(
                MockTokenFactory.Object,
                MockIdentity.Object);
        }

        [TestMethod]
        public void AcceptFormatVisitorGeneratesExpectedToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObject(
                nameof(SqlQueryKeyword.inserted),
                TestIdentityName), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorPassesToGeneratedToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
