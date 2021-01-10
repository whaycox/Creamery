﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Tests
{
    using Domain;
    using Expressions.Abstraction;
    using Template;

    [TestClass]
    public class ConvertNodeTest : BaseExpressionNodeTemplate<UnaryExpression>
    {
        private Expression TestOperandExpression = Expression.Constant(1);

        private Mock<IExpressionNode> MockOperandExpression = new Mock<IExpressionNode>();

        private UnaryExpression _testExpression = null;
        protected override UnaryExpression TestExpression => _testExpression;

        private ConvertNode TestObject = null;
        protected override BaseExpressionNode<UnaryExpression> BaseExpressionNodeTemplateTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            _testExpression = Expression.Convert(TestOperandExpression, typeof(int));

            MockNodeFactory
                .Setup(factory => factory.Build(TestOperandExpression))
                .Returns(MockOperandExpression.Object);

            TestObject = new ConvertNode(
                MockNodeFactory.Object,
                TestExpression);
        }

        protected override void VerifyAcceptVisitorWasProper() => MockVisitor
            .Verify(visitor => visitor.VisitConvert(TestObject), Times.Once);

        [TestMethod]
        public void BuildsNodeForOperand()
        {
            MockNodeFactory.Verify(factory => factory.Build(TestOperandExpression), Times.Once);
        }

        [TestMethod]
        public void SetsOperandNode()
        {
            Assert.AreSame(MockOperandExpression.Object, TestObject.Operand);
        }
    }
}
