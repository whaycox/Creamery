using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Domain;
    using Template;

    [TestClass]
    public class KeywordSqlQueryTokenTest : LiteralSqlQueryTokenTemplate
    {
        private SqlQueryKeyword TestKeyword = SqlQueryKeyword.INSERT;

        private KeywordSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new KeywordSqlQueryToken(TestKeyword);
        }

        [DataTestMethod]
        [DataRow(SqlQueryKeyword.CREATE)]
        [DataRow(SqlQueryKeyword.OUTPUT)]
        [DataRow(SqlQueryKeyword.WHERE)]
        [DataRow(SqlQueryKeyword.inserted)]
        public void LiteralIsKeyword(SqlQueryKeyword testKeyword)
        {
            TestObject = new KeywordSqlQueryToken(testKeyword);

            Assert.AreEqual(testKeyword.ToString(), TestObject.Literal);
        }

        [TestMethod]
        public void VisitsFormatterAsLiteral() => VerifyTokenAcceptsLiteralToken(TestObject);
    }
}
