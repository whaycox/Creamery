using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Tests
{
    using Abstraction;
    using Curds.Domain;
    using Implementation;

    [TestClass]
    public class ExpressionParserTest
    {
        private Expression<Func<TestEntity, int>> TestPropertySelectionExpression = (testEntity) => testEntity.IntValue;
        private PropertyInfo TestPropertyInfo = typeof(TestEntity).GetProperty(nameof(TestEntity.IntValue));

        private Mock<IExpressionNodeFactory> MockExpressionNodeFactory = new Mock<IExpressionNodeFactory>();
        private Mock<IExpressionNode> MockExpressionNode = new Mock<IExpressionNode>();
        private Mock<IExpressionVisitorFactory> MockExpressionVisitorFactory = new Mock<IExpressionVisitorFactory>();
        private Mock<IExpressionVisitor<PropertyInfo>> MockPropertySelectionVisitor = new Mock<IExpressionVisitor<PropertyInfo>>();

        private ExpressionParser TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockExpressionNodeFactory
                .Setup(factory => factory.Build(It.IsAny<Expression>()))
                .Returns(MockExpressionNode.Object);
            MockExpressionVisitorFactory
                .Setup(factory => factory.CreatePropertySelectionVisitor())
                .Returns(MockPropertySelectionVisitor.Object);
            MockPropertySelectionVisitor
                .Setup(visitor => visitor.Build())
                .Returns(TestPropertyInfo);

            TestObject = new ExpressionParser(
                MockExpressionNodeFactory.Object,
                MockExpressionVisitorFactory.Object);
        }

        [TestMethod]
        public void PropertySelectionExpressionParsesNodes()
        {
            TestObject.ParsePropertyExpression(TestPropertySelectionExpression);

            MockExpressionNodeFactory.Verify(factory => factory.Build(TestPropertySelectionExpression), Times.Once);
        }

        [TestMethod]
        public void PropertySelectionExpressionBuildsVisitor()
        {
            TestObject.ParsePropertyExpression(TestPropertySelectionExpression);

            MockExpressionVisitorFactory.Verify(factory => factory.CreatePropertySelectionVisitor(), Times.Once);
        }

        [TestMethod]
        public void PropertySelectionExpressionVisitsNode()
        {
            TestObject.ParsePropertyExpression(TestPropertySelectionExpression);

            MockExpressionNode.Verify(node => node.AcceptVisitor(MockPropertySelectionVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void PropertySelectionReturnsVisitorResult()
        {
            PropertyInfo actual = TestObject.ParsePropertyExpression(TestPropertySelectionExpression);

            Assert.AreEqual(TestPropertyInfo, actual);
        }
    }
}
