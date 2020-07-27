using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Persistence.Model.Tests
{
    using Domain;

    [TestClass]
    public class ModelExceptionTest
    {
        private Exception TestInnerException = new Exception();

        private ModelException TestObject = null;

        [TestMethod]
        public void SetsMessage()
        {
            TestObject = new ModelException(nameof(SetsMessage));

            Assert.AreEqual(nameof(SetsMessage), TestObject.Message);
        }

        [TestMethod]
        public void SetsMessageAndInnerException()
        {
            TestObject = new ModelException(nameof(SetsMessageAndInnerException), TestInnerException);

            Assert.AreEqual(nameof(SetsMessageAndInnerException), TestObject.Message);
            Assert.AreSame(TestInnerException, TestObject.InnerException);
        }

    }
}
