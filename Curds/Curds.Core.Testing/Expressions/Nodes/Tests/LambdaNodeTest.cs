using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Expressions.Abstraction;
    using Template;

    [TestClass]
    public class LambdaNodeTest : BaseExpressionNodeTemplate<LambdaExpression>
    {
        private Expression TestBodyExpression = Expression.Constant(1);
        private ParameterExpression TestParameterExpression = Expression.Parameter(typeof(int), nameof(TestParameterExpression));

        private Mock<IExpressionNode> MockBodyNode = new Mock<IExpressionNode>();

        private LambdaExpression _testExpression = null;
        protected override LambdaExpression TestExpression => _testExpression;

        private LambdaNode TestObject = null;
        protected override BaseExpressionNode<LambdaExpression> BaseExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.Lambda(TestBodyExpression, TestParameterExpression);

            MockNodeFactory
                .Setup(factory => factory.Build(TestBodyExpression))
                .Returns(MockBodyNode.Object);

            TestObject = new LambdaNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitLambda(TestObject), Times.Once);

        [TestMethod]
        public void BuildsNodeForBody()
        {
            MockNodeFactory.Verify(factory => factory.Build(TestBodyExpression), Times.Once);
        }

        [TestMethod]
        public void SetsBodyNode()
        {
            Assert.AreSame(MockBodyNode.Object, TestObject.Body);
        }
    }
}
