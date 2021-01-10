using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Template;

    [TestClass]
    public class ConstantNodeTest : BaseExpressionNodeTemplate<ConstantExpression>
    {
        protected override ConstantExpression TestExpression { get; } = Expression.Constant(1);

        private ConstantNode TestObject = null;
        protected override BaseExpressionNode<ConstantExpression> BaseExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ConstantNode(TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitConstant(TestObject), Times.Once);
    }
}
