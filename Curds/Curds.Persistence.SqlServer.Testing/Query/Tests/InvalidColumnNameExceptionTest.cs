using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Persistence.Query.Tests
{
    using Domain;

    [TestClass]
    public class InvalidColumnNameExceptionTest
    {
        private Exception TestInnerException = new Exception();

        private InvalidColumnNameException TestObject = null;

        [TestMethod]
        public void MessageIsExpected()
        {
            TestObject = new InvalidColumnNameException(nameof(MessageIsExpected));

            Assert.AreEqual(InvalidColumnNameException.ExceptionMessage(nameof(MessageIsExpected)), TestObject.Message);
        }

        [TestMethod]
        public void MessageIsExpectedWithInnerException()
        {
            TestObject = new InvalidColumnNameException(nameof(MessageIsExpectedWithInnerException), TestInnerException);

            Assert.AreEqual(InvalidColumnNameException.ExceptionMessage(nameof(MessageIsExpectedWithInnerException)), TestObject.Message);
            Assert.AreSame(TestInnerException, TestObject.InnerException);
        }
    }
}
