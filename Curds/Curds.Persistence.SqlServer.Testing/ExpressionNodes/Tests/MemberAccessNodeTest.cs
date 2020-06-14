using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.ExpressionNodes.Tests
{
    using Domain;
    using Persistence.Abstraction;
    using Template;

    [TestClass]
    public class MemberAccessNodeTest : BaseExpressionNodeTemplate<MemberExpression>
    {
        private ParameterExpression TestParameter = Expression.Parameter(typeof(DateTime), nameof(TestParameter));
        private MemberInfo TestMember = typeof(DateTime).GetProperty(nameof(DateTime.Ticks));

        private Mock<IExpressionNode> MockExpressionNode = new Mock<IExpressionNode>();

        private MemberExpression _testExpression = null;
        protected override MemberExpression TestExpression => _testExpression;

        private MemberAccessNode TestObject = null;
        protected override BaseExpressionNode<MemberExpression> BaseExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.MakeMemberAccess(TestParameter, TestMember);

            MockNodeFactory
                .Setup(factory => factory.Build(TestParameter))
                .Returns(MockExpressionNode.Object);

            TestObject = new MemberAccessNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitMemberAccess(TestObject), Times.Once);

        protected override void SetupVisitReturn() => MockVisitor
            .Setup(visitor => visitor.VisitMemberAccess(It.IsAny<MemberAccessNode>()))
            .Returns(TestVisitReturn);

        [TestMethod]
        public void BuildsExpressionNode()
        {
            MockNodeFactory.Verify(factory => factory.Build(TestParameter), Times.Once);
        }

        [TestMethod]
        public void SetsExpressionNode()
        {
            Assert.AreSame(MockExpressionNode.Object, TestObject.Expression);
        }
    }
}
