using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class ObjectNameSqlQueryTokenTest : ObjectNameSqlQueryTokenTemplate
    {
        private string TestName = nameof(TestName);

        private ObjectNameSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ObjectNameSqlQueryToken(TestName);
        }

        [TestMethod]
        public void LiteralWrapsNameInIdentifiers() => VerifyLiteralWrapsNameInIdentifiers(TestObject);

        [TestMethod]
        public void VisitsFormatterAsLiteral() => VerifyTokenAcceptsLiteralToken(TestObject);
    }
}
