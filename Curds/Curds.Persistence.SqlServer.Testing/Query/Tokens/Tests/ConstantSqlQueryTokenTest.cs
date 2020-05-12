using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class ConstantSqlQueryTokenTest : LiteralSqlQueryTokenTemplate
    {
        private string TestConstant = nameof(TestConstant);

        private ConstantSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ConstantSqlQueryToken(TestConstant);
        }

        [TestMethod]
        public void LiteralIsConstant()
        {
            Assert.AreEqual(TestConstant, TestObject.Literal);
        }

        [TestMethod]
        public void VisitsFormatterAsLiteral() => VerifyTokenAcceptsLiteralToken(TestObject);
    }
}
