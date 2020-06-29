using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Whey;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Query.Domain;
    using Template;

    [TestClass]
    public class ArithmeticOperationSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private ArithmeticOperation TestOperation = ArithmeticOperation.Equals;

        private Mock<ISqlQueryToken> MockLeftToken = new Mock<ISqlQueryToken>();
        private Mock<ISqlQueryToken> MockRightToken = new Mock<ISqlQueryToken>();

        private ArithmeticOperationSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ArithmeticOperationSqlQueryToken(
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
        [DataRow(ArithmeticOperation.Equals, " = ")]
        [DataRow(ArithmeticOperation.Modulo, " % ")]
        public void BuildsOperationToken(ArithmeticOperation testOperation, string expectedLiteral)
        {
            TestOperation = testOperation;

            TestObject = new ArithmeticOperationSqlQueryToken(
                TestOperation,
                MockLeftToken.Object,
                MockRightToken.Object);

            ConstantSqlQueryToken actual = TestObject.Operation.VerifyIsActually<ConstantSqlQueryToken>();
            Assert.AreEqual(expectedLiteral, actual.Literal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidOperationThrows()
        {
            TestObject = new ArithmeticOperationSqlQueryToken(
                (ArithmeticOperation)99,
                MockLeftToken.Object,
                MockRightToken.Object);
        }
    }
}
