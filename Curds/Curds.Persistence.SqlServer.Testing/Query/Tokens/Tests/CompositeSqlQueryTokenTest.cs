using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class CompositeSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private SimpleCompositeSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SimpleCompositeSqlQueryToken(MockTokenFactory.Object);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void AcceptFormatVisitorPassesOnToEachGeneratedToken(int tokensToGenerate)
        {
            for (int i = 0; i < tokensToGenerate; i++)
                TestObject.GeneratedTokens.Add(MockToken.Object);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Exactly(tokensToGenerate));
        }
    }
}
