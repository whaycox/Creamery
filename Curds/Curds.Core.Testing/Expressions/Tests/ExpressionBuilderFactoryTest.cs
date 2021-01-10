using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Expressions.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class ExpressionBuilderFactoryTest
    {
        private ExpressionBuilderFactory TestObject = new ExpressionBuilderFactory();

        [TestMethod]
        public void ReturnsExpectedType()
        {
            IExpressionBuilder actual = TestObject.Create();

            Assert.IsInstanceOfType(actual, typeof(ExpressionBuilder));
        }
    }
}
