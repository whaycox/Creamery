using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class ParameterSqlQueryTokenTest : LiteralSqlQueryTokenTemplate
    {
        private string TestName = nameof(TestName);

        private ParameterSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ParameterSqlQueryToken(TestName);
        }

        [TestMethod]
        public void LiteralPrependsAtToName()
        {
            Assert.AreEqual($"@{TestName}", TestObject.Literal);
        }

        [TestMethod]
        public void VisitsFormatterAsLiteral() => VerifyTokenAcceptsLiteralToken(TestObject);
    }
}
