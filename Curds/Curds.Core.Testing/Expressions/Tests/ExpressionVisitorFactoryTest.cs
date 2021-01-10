using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Reflection;

namespace Curds.Expressions.Tests
{
    using Implementation;
    using Abstraction;

    [TestClass]
    public class ExpressionVisitorFactoryTest
    {
        private ExpressionVisitorFactory TestObject = new ExpressionVisitorFactory();

        [TestMethod]
        public void PropertySelectionReturnsExpectedType()
        {
            IExpressionVisitor<PropertyInfo> actual = TestObject.CreatePropertySelectionVisitor();

            Assert.IsInstanceOfType(actual, typeof(PropertySelectionVisitor));
        }
    }
}
