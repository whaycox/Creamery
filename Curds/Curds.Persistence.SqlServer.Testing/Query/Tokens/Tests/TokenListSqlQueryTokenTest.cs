using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class TokenListSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private List<ISqlQueryToken> TestTokens = new List<ISqlQueryToken>();

        private TokenListSqlQueryToken TestObject = null;

        private void BuildTestObject()
        {
            TestObject = new TokenListSqlQueryToken(TestTokens);
        }

        [TestMethod]
        public void GroupingStartsFalse()
        {
            BuildTestObject();

            Assert.IsFalse(TestObject.IncludeGrouping);
        }

        [TestMethod]
        public void SeparatorsStartsTrue()
        {
            BuildTestObject();

            Assert.IsTrue(TestObject.IncludeSeparators);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(13)]
        [DataRow(17)]
        public void TokensAreFromConstructor(int tokens)
        {
            for (int i = 0; i < tokens; i++)
                TestTokens.Add(Mock.Of<ISqlQueryToken>());

            BuildTestObject();

            CollectionAssert.AreEqual(TestTokens, TestObject.Tokens);
        }

        [TestMethod]
        public void VisitsFormatterAsTokenList()
        {
            BuildTestObject();

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitTokenList(TestObject), Times.Once);
        }
    }
}
