using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Template
{
    using Domain;
    using Persistence.Abstraction;

    public abstract class BaseBinaryExpressionNodeTemplate : BaseExpressionNodeTemplate<BinaryExpression>
    {
        protected Expression TestLeftExpression = Expression.Constant(1);
        protected Expression TestRightExpression = Expression.Constant(0);

        protected Mock<IExpressionNode> TestLeftNode = new Mock<IExpressionNode>();
        protected Mock<IExpressionNode> TestRightNode = new Mock<IExpressionNode>();

        protected sealed override BaseExpressionNode<BinaryExpression> BaseExpressionNodeTemplateTestObject => BaseBinaryExpressionNodeTemplateTestObject;
        protected abstract BaseBinaryExpressionNode BaseBinaryExpressionNodeTemplateTestObject { get; }

        [TestInitialize]
        public void SetupBaseBinaryExpressionNodeTemplate()
        {
            MockNodeFactory
                .Setup(factory => factory.Build(TestLeftExpression))
                .Returns(TestLeftNode.Object);
            MockNodeFactory
                .Setup(factory => factory.Build(TestRightExpression))
                .Returns(TestRightNode.Object);
        }

        [TestMethod]
        public void BuildsNodeForLeft()
        {
            MockNodeFactory.Verify(factory => factory.Build(TestLeftExpression), Times.Once);
        }

        [TestMethod]
        public void SetsLeftNode()
        {
            Assert.AreSame(TestLeftNode.Object, BaseBinaryExpressionNodeTemplateTestObject.Left);
        }

        [TestMethod]
        public void BuildsNodeForRight()
        {
            MockNodeFactory.Verify(factory => factory.Build(TestRightExpression), Times.Once);
        }

        [TestMethod]
        public void SetsRightNode()
        {
            Assert.AreSame(TestRightNode.Object, BaseBinaryExpressionNodeTemplateTestObject.Right);
        }
    }
}
