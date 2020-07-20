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

        [TestMethod]
        public void VisitColumnListFormatsByDefault()
        {
            throw new System.NotImplementedException();
            //ColumnListSqlQueryToken testColumnList = BuildTestColumnListToken(0);

            //TestObject.VisitColumnList(testColumnList);

            //TestStringBuilder
            //    .VerifySetNewLine()
            //    .VerifyCreateScope()
            //    .VerifyDisposeScope()
            //    .VerifyOperationCount();
        }

        [TestMethod]
        public void VisitColumnListWithGroupingFormatsByDefault()
        {
            throw new System.NotImplementedException();
            //ColumnListSqlQueryToken testColumnList = BuildTestColumnListToken(0);
            //testColumnList.IncludeGrouping = true;

            //TestObject.VisitColumnList(testColumnList);

            //TestStringBuilder
            //    .VerifySetNewLine()
            //    .VerifyAppendLine("(")
            //    .VerifyCreateScope()
            //    .VerifyDisposeScope()
            //    .VerifySetNewLine()
            //    .VerifyAppend(")")
            //    .VerifyOperationCount();
        }

        [TestMethod]
        public void VisitColumnListNeedsRethinking()
        {
            Assert.Fail();
        }
    }
}
