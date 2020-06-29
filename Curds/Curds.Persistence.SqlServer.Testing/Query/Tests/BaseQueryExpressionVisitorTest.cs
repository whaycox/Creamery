using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Abstraction;

    [TestClass]
    public class BaseQueryExpressionVisitorTest
    {
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();

        private SimpleQueryExpressionVisitor TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SimpleQueryExpressionVisitor(MockQueryContext.Object);
        }

        [TestMethod]
        public void SetsQueryContext()
        {
            Assert.AreSame(MockQueryContext.Object, TestObject.Context);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitConstantThrows()
        {
            TestObject.VisitConstant(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitConvertThrows()
        {
            TestObject.VisitConvert(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitEqualThrows()
        {
            TestObject.VisitEqual(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitLessThanThrows()
        {
            TestObject.VisitLessThan(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitLessThanOrEqualThrows()
        {
            TestObject.VisitLessThanOrEqual(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitLambdaThrows()
        {
            TestObject.VisitLambda(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitMemberAccessThrows()
        {
            TestObject.VisitMemberAccess(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitParameterThrows()
        {
            TestObject.VisitParameter(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VisitModuloThrows()
        {
            TestObject.VisitModulo(null);
        }
    }
}
