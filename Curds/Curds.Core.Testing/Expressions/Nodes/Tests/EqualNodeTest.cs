using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Template;

    [TestClass]
    public class EqualNodeTest : BaseBinaryExpressionNodeTemplate
    {
        private BinaryExpression _testExpression = null;
        protected override BinaryExpression TestExpression => _testExpression;

        private EqualNode TestObject = null;
        protected override BaseBinaryExpressionNode BaseBinaryExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.Equal(TestLeftExpression, TestRightExpression);

            TestObject = new EqualNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitEqual(TestObject), Times.Once);
    }
}
