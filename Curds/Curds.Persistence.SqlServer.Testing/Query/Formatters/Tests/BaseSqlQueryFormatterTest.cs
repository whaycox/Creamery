using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Formatters.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class BaseSqlQueryFormatterTest : BaseSqlQueryFormatterTemplate
    {
        private List<ISqlQueryToken> TestTokens = new List<ISqlQueryToken>();

        private Mock<ISqlQueryToken> MockToken = new Mock<ISqlQueryToken>();

        private SimpleSqlQueryFormatter TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestTokens.Add(MockToken.Object);

            TestObject = new SimpleSqlQueryFormatter(TestStringBuilder);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(8)]
        [DataRow(10)]
        [DataRow(17)]
        public void FormatAcceptsFormatVisitorForEachToken(int tokensToSupply)
        {
            TestTokens.Clear();
            for (int i = 0; i < tokensToSupply; i++)
                TestTokens.Add(MockToken.Object);

            TestObject.FormatTokens(TestTokens);

            MockToken.Verify(token => token.AcceptFormatVisitor(TestObject), Times.Exactly(tokensToSupply));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(8)]
        [DataRow(10)]
        [DataRow(17)]
        public void FormatGeneratesExpectedOperations(int tokensToSupply)
        {
            TestTokens.Clear();
            for (int i = 0; i < tokensToSupply; i++)
                TestTokens.Add(MockToken.Object);

            TestObject.FormatTokens(TestTokens);

            for (int i = 0; i < tokensToSupply; i++)
                TestStringBuilder.VerifySetNewLine();
            TestStringBuilder.VerifyFlush();
            TestStringBuilder.VerifyOperationCount();
        }
    }
}
