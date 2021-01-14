using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Expressions.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class ExpressionBuilderFactoryTest
    {
        private Mock<IExpressionFactory> MockExpressionFactory = new Mock<IExpressionFactory>();

        private ExpressionBuilderFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ExpressionBuilderFactory(MockExpressionFactory.Object);
        }

        [TestMethod]
        public void ReturnsExpectedType()
        {
            IExpressionBuilder actual = TestObject.Create();

            Assert.IsInstanceOfType(actual, typeof(ExpressionBuilder));
        }
    }
}
