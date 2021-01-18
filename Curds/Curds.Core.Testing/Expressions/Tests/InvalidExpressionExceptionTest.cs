using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace Curds.Expressions.Tests
{
    using Domain;

    [TestClass]
    public class InvalidExpressionExceptionTest
    {
        private string BaseExpectedMessage = $"{nameof(TestExpression)} is not a valid expression";
        private Expression TestExpression = Expression.Parameter(typeof(Expression), nameof(TestExpression));
        private Exception TestInnerException = new Exception();

        private InvalidExpressionException TestObject = null;

        [TestMethod]
        public void MessageWithoutReasonIsExpected()
        {
            TestObject = new InvalidExpressionException(TestExpression);

            Assert.AreEqual(BaseExpectedMessage, TestObject.Message);
        }

        [TestMethod]
        public void MessageWithReasonIsExpected()
        {
            TestObject = new InvalidExpressionException(TestExpression, nameof(MessageWithReasonIsExpected));

            Assert.AreEqual($"{BaseExpectedMessage}: {nameof(MessageWithReasonIsExpected)}", TestObject.Message);
        }

        [TestMethod]
        public void MessageWithoutReasonIsExpectedWithInnerException()
        {
            TestObject = new InvalidExpressionException(TestExpression, TestInnerException);

            Assert.AreSame(TestInnerException, TestObject.InnerException);
            Assert.AreEqual(BaseExpectedMessage, TestObject.Message);
        }

        [TestMethod]
        public void MessageWithReasonIsExpectedWithInnerException()
        {
            TestObject = new InvalidExpressionException(TestExpression, TestInnerException, nameof(MessageWithReasonIsExpectedWithInnerException));

            Assert.AreSame(TestInnerException, TestObject.InnerException);
            Assert.AreEqual($"{BaseExpectedMessage}: {nameof(MessageWithReasonIsExpectedWithInnerException)}", TestObject.Message);
        }
    }
}
