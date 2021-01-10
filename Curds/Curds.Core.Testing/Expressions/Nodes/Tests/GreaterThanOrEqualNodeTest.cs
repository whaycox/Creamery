using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Template;

    [TestClass]
    public class GreaterThanOrEqualNodeTest : BaseBinaryExpressionNodeTemplate
    {
        private BinaryExpression _testExpression = null;
        protected override BinaryExpression TestExpression => _testExpression;

        private GreaterThanOrEqualNode TestObject = null;
        protected override BaseBinaryExpressionNode BaseBinaryExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.GreaterThanOrEqual(TestLeftExpression, TestRightExpression);

            TestObject = new GreaterThanOrEqualNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitGreaterThanOrEqual(TestObject), Times.Once);
    }
}
