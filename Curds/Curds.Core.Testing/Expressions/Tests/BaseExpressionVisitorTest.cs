using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Expressions.Tests
{
    using Implementation;

    [TestClass]
    public class BaseExpressionVisitorTest
    {
        private SimpleBaseExpressionVisitor TestObject = new SimpleBaseExpressionVisitor();

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConstantIsntImplemented()
        {
            TestObject.VisitConstant(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertIsntImplemented()
        {
            TestObject.VisitConvert(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void EqualIsntImplemented()
        {
            TestObject.VisitEqual(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void NotEqualIsntImplemented()
        {
            TestObject.VisitNotEqual(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void LessThanIsntImplemented()
        {
            TestObject.VisitLessThan(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void LessThanOrEqualIsntImplemented()
        {
            TestObject.VisitLessThanOrEqual(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GreateThanIsntImplemented()
        {
            TestObject.VisitGreaterThan(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GreaterThanOrEqualIsntImplemented()
        {
            TestObject.VisitGreaterThanOrEqual(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void LambdaIsntImplemented()
        {
            TestObject.VisitLambda(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void MemberAccessIsntImplemented()
        {
            TestObject.VisitMemberAccess(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ParameterIsntImplemented()
        {
            TestObject.VisitParameter(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ModuloIsntImplemented()
        {
            TestObject.VisitModulo(null);
        }
    }
}
