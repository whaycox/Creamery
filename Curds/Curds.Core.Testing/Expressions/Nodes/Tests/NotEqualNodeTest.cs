using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Template;

    [TestClass]
    public class NotEqualNodeTest : BaseBinaryExpressionNodeTemplate
    {
        private BinaryExpression _testExpression = null;
        protected override BinaryExpression TestExpression => _testExpression;

        private NotEqualNode TestObject = null;
        protected override BaseBinaryExpressionNode BaseBinaryExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.NotEqual(TestLeftExpression, TestRightExpression);

            TestObject = new NotEqualNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitNotEqual(TestObject), Times.Once);
    }
}
