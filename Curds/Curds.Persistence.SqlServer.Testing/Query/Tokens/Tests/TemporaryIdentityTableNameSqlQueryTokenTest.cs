using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class TemporaryIdentityTableNameSqlQueryTokenTest : ObjectNameSqlQueryTokenTemplate
    {
        private string TestTableName = nameof(TestTableName);

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private TemporaryIdentityTableNameSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockTable
                .Setup(table => table.Name)
                .Returns(TestTableName);

            TestObject = new TemporaryIdentityTableNameSqlQueryToken(MockTable.Object);
        }

        [TestMethod]
        public void BaseTableNameIsFromConstructor()
        {
            Assert.AreEqual(TestTableName, TestObject.BaseTableName);
        }

        [TestMethod]
        public void NameIsFormattedBaseTableName()
        {
            Assert.AreEqual(TemporaryIdentityTableNameSqlQueryToken.FormatName(TestTableName), TestObject.Name);
        }

        [TestMethod]
        public void LiteralWrapsNameInIdentifiers() => VerifyLiteralWrapsNameInIdentifiers(TestObject);

        [TestMethod]
        public void VisitsFormatterAsLiteral() => VerifyTokenAcceptsLiteralToken(TestObject);
    }
}
