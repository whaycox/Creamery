using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Formatters.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Text.Abstraction;
    using Tokens.Implementation;

    [TestClass]
    public class ProperSqlQueryFormatterTest
    {
        private SimpleIndentStringBuilder TestStringBuilder = new SimpleIndentStringBuilder();
        private List<ISqlQueryToken> TestTokens = new List<ISqlQueryToken>();

        private Mock<ISqlQueryToken> MockToken = new Mock<ISqlQueryToken>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private ProperSqlQueryFormatter TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ProperSqlQueryFormatter(TestStringBuilder);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(16)]
        [DataRow(20)]
        [DataRow(50)]
        public void FormatTokensVisitsEach(int tokensToFormat)
        {
            for (int i = 0; i < tokensToFormat; i++)
                TestTokens.Add(MockToken.Object);

            TestObject.FormatTokens(TestTokens);

            MockToken.Verify(token => token.AcceptFormatVisitor(TestObject), Times.Exactly(tokensToFormat));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(16)]
        [DataRow(20)]
        [DataRow(50)]
        public void FormatTokensSetsNewLineForEach(int tokensToFormat)
        {
            for (int i = 0; i < tokensToFormat; i++)
                TestTokens.Add(MockToken.Object);

            TestObject.FormatTokens(TestTokens);

            for (int i = 0; i < tokensToFormat; i++)
                TestStringBuilder.VerifySetNewLine();
        }

        [TestMethod]
        public void FormatTokensFlushesStringBuilder()
        {
            TestObject.FormatTokens(TestTokens);

            TestStringBuilder
                .VerifyFlush()
                .VerifyOperationCount();
        }

        [TestMethod]
        public void FormatTokensReturnsFlushedText()
        {
            Assert.AreEqual(nameof(TestStringBuilder.Flush), TestObject.FormatTokens(TestTokens));
        }

        [TestMethod]
        public void VisitLiteralAppendsToBuilder()
        {
            LiteralSqlQueryToken testLiteral = new ConstantSqlQueryToken(nameof(VisitLiteralAppendsToBuilder));

            TestObject.VisitLiteral(testLiteral);

            TestStringBuilder
                .VerifyAppend(testLiteral.Literal)
                .VerifyOperationCount();
        }

        private TokenListSqlQueryToken TestTokenListToken(int tokensToAdd)
        {
            for (int i = 0; i < tokensToAdd; i++)
                TestTokens.Add(MockToken.Object);

            return new TokenListSqlQueryToken(TestTokens);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void VisitTokenListVisitsEachElement(int tokensInList)
        {
            TokenListSqlQueryToken testToken = TestTokenListToken(tokensInList);

            TestObject.VisitTokenList(testToken);

            MockToken.Verify(token => token.AcceptFormatVisitor(TestObject), Times.Exactly(tokensInList));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void VisitTokenListWithoutGroupingOrSeparatorsIsExpected(int tokensInList)
        {
            TokenListSqlQueryToken testToken = TestTokenListToken(tokensInList);
            testToken.IncludeGrouping = false;
            testToken.IncludeSeparators = false;

            TestObject.VisitTokenList(testToken);

            TestStringBuilder
                .VerifySetNewLine()
                .VerifyCreateScope();
            for (int i = 0; i < tokensInList - 1; i++)
                TestStringBuilder.VerifySetNewLine();
            TestStringBuilder
                .VerifyDisposeScope()
                .VerifyOperationCount();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void VisitTokenListWithoutGroupingButWithSeparatorsIsExpected(int tokensInList)
        {
            TokenListSqlQueryToken testToken = TestTokenListToken(tokensInList);
            testToken.IncludeGrouping = false;
            testToken.IncludeSeparators = true;

            TestObject.VisitTokenList(testToken);

            TestStringBuilder
                .VerifySetNewLine()
                .VerifyCreateScope();
            for (int i = 0; i < tokensInList; i++)
            {
                if (tokensInList > 1)
                    TestStringBuilder.VerifyAppend(i == 0 ? " " : ",");
                if (i < tokensInList - 1)
                    TestStringBuilder.VerifySetNewLine();
            }
            TestStringBuilder
                .VerifyDisposeScope()
                .VerifyOperationCount();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void VisitTokenListWithGroupingButWithoutSeparatorsIsExpected(int tokensInList)
        {
            TokenListSqlQueryToken testToken = TestTokenListToken(tokensInList);
            testToken.IncludeGrouping = true;
            testToken.IncludeSeparators = false;

            TestObject.VisitTokenList(testToken);

            TestStringBuilder
                .VerifySetNewLine()
                .VerifyAppendLine("(")
                .VerifyCreateScope();
            for (int i = 0; i < tokensInList - 1; i++)
                TestStringBuilder.VerifySetNewLine();
            TestStringBuilder
                .VerifyDisposeScope()
                .VerifySetNewLine()
                .VerifyAppend(")")
                .VerifyOperationCount();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void VisitTokenListWithGroupingAndSeparatorsIsExpected(int tokensInList)
        {
            TokenListSqlQueryToken testToken = TestTokenListToken(tokensInList);
            testToken.IncludeGrouping = true;
            testToken.IncludeSeparators = true;

            TestObject.VisitTokenList(testToken);

            TestStringBuilder
                .VerifySetNewLine()
                .VerifyAppendLine("(")
                .VerifyCreateScope();
            for (int i = 0; i < tokensInList; i++)
            {
                if (tokensInList > 1)
                    TestStringBuilder.VerifyAppend(i == 0 ? " " : ",");
                if (i < tokensInList - 1)
                    TestStringBuilder.VerifySetNewLine();
            }
            TestStringBuilder
                .VerifyDisposeScope()
                .VerifySetNewLine()
                .VerifyAppend(")")
                .VerifyOperationCount();
        }
    }
}
