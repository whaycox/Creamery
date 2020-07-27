using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Domain;
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class BooleanComparisonSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private BooleanComparison TestComparison = BooleanComparison.Equals;

        private Mock<ISqlQueryToken> MockLeftToken = new Mock<ISqlQueryToken>();
        private Mock<ISqlQueryToken> MockRightToken = new Mock<ISqlQueryToken>();

        private BooleanComparisonSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            SetupTokenFactoryForMockToken(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));

            BuildTestObject(TestComparison);
        }
        private void BuildTestObject(BooleanComparison testComparison)
        {
            TestObject = new BooleanComparisonSqlQueryToken(
                MockTokenFactory.Object,
                testComparison,
                MockLeftToken.Object,
                MockRightToken.Object);
        }

        [TestMethod]
        public void AcceptFormatVisitorGeneratesExpectedPhrase()
        {
            SetupTokenFactoryForMockToken(factory => factory.Keyword(It.IsAny<SqlQueryKeyword>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(
                MockLeftToken.Object,
                MockToken.Object,
                MockRightToken.Object), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorPassesOnToGeneratedPhrase()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(BooleanComparison.Equals, SqlQueryKeyword.Equals)]
        [DataRow(BooleanComparison.NotEquals, SqlQueryKeyword.NotEquals)]
        [DataRow(BooleanComparison.GreaterThan, SqlQueryKeyword.GreaterThan)]
        [DataRow(BooleanComparison.GreaterThanOrEquals, SqlQueryKeyword.GreaterThanOrEquals)]
        [DataRow(BooleanComparison.LessThan, SqlQueryKeyword.LessThan)]
        [DataRow(BooleanComparison.LessThanOrEquals, SqlQueryKeyword.LessThanOrEquals)]
        public void AcceptFormatVisitorGeneratesExpectedKeyword(BooleanComparison testComparison, SqlQueryKeyword expectedKeyword)
        {
            BuildTestObject(testComparison);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(expectedKeyword), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidOperationThrowsOnAcceptFormatVisitor()
        {
            BuildTestObject((BooleanComparison)99);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);
        }
    }
}
