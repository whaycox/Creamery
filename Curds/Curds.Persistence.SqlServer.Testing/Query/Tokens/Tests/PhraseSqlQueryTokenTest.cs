using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class PhraseSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private List<ISqlQueryToken> TestTokens = new List<ISqlQueryToken>();

        private PhraseSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            BuildTestObject();
        }
        private void BuildTestObject()
        {
            TestObject = new PhraseSqlQueryToken(
                MockTokenFactory.Object,
                TestTokens);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(7)]
        [DataRow(16)]
        public void TokensComeFromConstructor(int tokensToAdd)
        {
            for (int i = 0; i < 5; i++)
                TestTokens.Add(Mock.Of<ISqlQueryToken>());
            BuildTestObject();

            CollectionAssert.AreEqual(TestTokens, TestObject.Tokens);
        }

        [TestMethod]
        public void AcceptFormatVisitorWithSingleTokenPassesToToken()
        {
            TestTokens.Add(MockToken.Object);
            BuildTestObject();

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void MultipleTokensInterspersedWithSpaces()
        {
            TestTokens.Add(MockToken.Object);
            TestTokens.Add(MockToken.Object);
            BuildTestObject();

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Exactly(2));
            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<LiteralSqlQueryToken>(token => token.Literal == " ")), Times.Once);
        }
    }
}
