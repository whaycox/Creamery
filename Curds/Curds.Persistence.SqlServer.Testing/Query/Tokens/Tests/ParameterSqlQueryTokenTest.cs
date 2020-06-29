using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class ParameterSqlQueryTokenTest : LiteralSqlQueryTokenTemplate
    {
        private string TestName = nameof(TestName);
        private Type TestType = typeof(string);

        private ParameterSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ParameterSqlQueryToken(TestName, TestType);
        }

        [TestMethod]
        public void SetsNameFromConstructor()
        {
            Assert.AreEqual(TestName, TestObject.Name);
        }

        [TestMethod]
        public void SetsTypeFromConstructor()
        {
            Assert.AreEqual(TestType, TestObject.Type);
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
