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
        private Mock<ISqlQueryToken> MockToken = new Mock<ISqlQueryToken>();

        private PhraseSqlQueryToken TestObject = null;

        [TestMethod]
        public void TokensComeFromConstructor()
        {
            List<ISqlQueryToken> testTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < 5; i++)
                testTokens.Add(Mock.Of<ISqlQueryToken>());
            TestObject = new PhraseSqlQueryToken(
                testTokens[0],
                testTokens[1],
                testTokens[2],
                testTokens[3],
                testTokens[4]);

            CollectionAssert.AreEqual(testTokens, TestObject.Tokens);
        }

        [TestMethod]
        public void SingleTokenForwardsVisitorToToken()
        {
            TestObject = new PhraseSqlQueryToken(MockToken.Object);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void MultipleTokensInterspersedWithSpaces()
        {
            TestObject = new PhraseSqlQueryToken(MockToken.Object, MockToken.Object);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Exactly(2));
            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<LiteralSqlQueryToken>(token => token.Literal == " ")), Times.Once);
        }
    }
}
