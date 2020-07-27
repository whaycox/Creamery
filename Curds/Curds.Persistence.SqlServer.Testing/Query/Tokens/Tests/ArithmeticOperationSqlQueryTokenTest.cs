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
            SetupTokenFactoryForMockToken(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));

            BuildTestObject(TestOperation);
        }
        private void BuildTestObject(ArithmeticOperation testOperation)
        {
            TestObject = new ArithmeticOperationSqlQueryToken(
                MockTokenFactory.Object,
                testOperation,
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
        public void AcceptFormatVisitorPassesOnToPhrase()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(ArithmeticOperation.Equals, SqlQueryKeyword.Equals)]
        [DataRow(ArithmeticOperation.Modulo, SqlQueryKeyword.Modulo)]
        public void AcceptFormatVisitorGeneratesExpectedKeyword(ArithmeticOperation testOperation, SqlQueryKeyword expectedKeyword)
        {
            BuildTestObject(testOperation);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(expectedKeyword), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidOperationThrowsOnAcceptFormatVisitor()
        {
            BuildTestObject((ArithmeticOperation)99);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);
        }
    }
}
