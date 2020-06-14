using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Tests
{
    using Domain;
    using Template;

    [TestClass]
    public class LessThanNodeTest : BaseBinaryExpressionNodeTemplate
    {
        private BinaryExpression _testExpression = null;
        protected override BinaryExpression TestExpression => _testExpression;

        private LessThanNode TestObject = null;
        protected override BaseBinaryExpressionNode BaseBinaryExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.LessThan(TestLeftExpression, TestRightExpression);

            TestObject = new LessThanNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitLessThan(TestObject), Times.Once);

        protected override void SetupVisitReturn() => MockVisitor
            .Setup(visitor => visitor.VisitLessThan(TestObject))
            .Returns(TestVisitReturn);
    }
}
