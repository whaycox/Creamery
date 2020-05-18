using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Domain;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class BooleanSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private BooleanComparison TestOperation = BooleanComparison.Equals;

        private Mock<ISqlQueryToken> MockLeftToken = new Mock<ISqlQueryToken>();
        private Mock<ISqlQueryToken> MockRightToken = new Mock<ISqlQueryToken>();

        private BooleanComparisonSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new BooleanComparisonSqlQueryToken(
                TestOperation,
                MockLeftToken.Object,
                MockRightToken.Object);
        }

        [TestMethod]
        public void AcceptFormatVisitorForwardsToLeft()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockLeftToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorForwardsToRight()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockRightToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(BooleanComparison.Equals, "=")]
        [DataRow(BooleanComparison.NotEquals, "<>")]
        [DataRow(BooleanComparison.GreaterThan, ">")]
        [DataRow(BooleanComparison.GreaterThanOrEqual, ">=")]
        [DataRow(BooleanComparison.LessThan, "<")]
        [DataRow(BooleanComparison.LessThanOrEqual, "<=")]
        public void AcceptFormatVisitorVisitsExpectedOperation(BooleanComparison testOperation, string expectedLiteral)
        {
            TestObject = new BooleanComparisonSqlQueryToken(
                testOperation,
                MockLeftToken.Object,
                MockRightToken.Object);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<LiteralSqlQueryToken>(token => token.Literal == expectedLiteral)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidOperationThrows()
        {
            TestObject = new BooleanComparisonSqlQueryToken(
                (BooleanComparison)99,
                MockLeftToken.Object,
                MockRightToken.Object);
        }

    }
}
