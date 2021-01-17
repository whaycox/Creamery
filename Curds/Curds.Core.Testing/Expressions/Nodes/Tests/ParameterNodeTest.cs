using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Template;

    [TestClass]
    public class ParameterNodeTest : BaseExpressionNodeTemplate<ParameterExpression>
    {
        private ParameterExpression _testExpression = null;
        protected override ParameterExpression TestExpression => _testExpression;

        private ParameterNode TestObject = null;
        protected override BaseExpressionNode<ParameterExpression> BaseExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.Parameter(typeof(int), nameof(TestExpression));

            TestObject = new ParameterNode(TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitParameter(TestObject), Times.Once);
    }
}
