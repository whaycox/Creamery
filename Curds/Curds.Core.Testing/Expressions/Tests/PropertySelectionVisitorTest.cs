using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Tests
{
    using Abstraction;
    using Curds.Domain;
    using Implementation;
    using Nodes.Domain;

    [TestClass]
    public class PropertySelectionVisitorTest
    {
        private Expression<Func<TestEntity, int>> TestExpression = null;
        private ParameterExpression TestParameterExpression = null;
        private LambdaNode TestLambdaNode = null;
        private MemberExpression TestMemberExpression = null;
        private MemberAccessNode TestMemberAccessNode = null;
        private PropertyInfo TestProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.IntValue));

        private Mock<IExpressionNodeFactory> MockNodeFactory = new Mock<IExpressionNodeFactory>();
        private Mock<IExpressionNode> MockLambdaBodyNode = new Mock<IExpressionNode>();
        private Mock<IExpressionNode> MockMemberExpressionNode = new Mock<IExpressionNode>();

        private PropertySelectionVisitor TestObject = new PropertySelectionVisitor();

        [TestInitialize]
        public void Init()
        {
            SetupForExpression((entity) => entity.IntValue);
        }
        private void SetupForExpression(Expression<Func<TestEntity, int>> testExpression)
        {
            TestExpression = testExpression;
            TestParameterExpression = TestExpression
                .Parameters
                .Single();
            MockNodeFactory
                .Setup(factory => factory.Build(TestExpression.Body))
                .Returns(MockLambdaBodyNode.Object);
            TestLambdaNode = new LambdaNode(MockNodeFactory.Object, TestExpression);

            TestMemberExpression = (MemberExpression)TestExpression.Body;
            MockNodeFactory
                .Setup(factory => factory.Build(TestMemberExpression.Expression))
                .Returns(MockMemberExpressionNode.Object);
            TestMemberAccessNode = new MemberAccessNode(MockNodeFactory.Object, TestMemberExpression);
        }

        [TestMethod]
        public void VisitingLambdaNodeSetsLambdaParameter()
        {
            TestObject.VisitLambda(TestLambdaNode);

            Assert.AreEqual(TestParameterExpression, TestObject.LambdaParameter);
        }

        [TestMethod]
        public void VisitingLambdaVisitsLambdaBodyNode()
        {
            TestObject.VisitLambda(TestLambdaNode);

            MockLambdaBodyNode.Verify(node => node.AcceptVisitor(TestObject), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VisitingLambdaTwiceThrows()
        {
            TestObject.VisitLambda(TestLambdaNode);
            TestObject.VisitMemberAccess(TestMemberAccessNode);

            TestObject.VisitLambda(TestLambdaNode);
        }

        [TestMethod]
        public void VisitingMemberAccessSetsSelectedProperty()
        {
            TestObject.VisitLambda(TestLambdaNode);

            TestObject.VisitMemberAccess(TestMemberAccessNode);

            Assert.AreEqual(TestProperty, TestObject.SelectedProperty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VisitingMemberAccessFirstThrows()
        {
            TestObject.VisitMemberAccess(TestMemberAccessNode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VisitingFieldAccessThrows()
        {
            SetupForExpression((entity) => entity.TestField);
            TestObject.VisitLambda(TestLambdaNode);

            TestObject.VisitMemberAccess(TestMemberAccessNode);
        }

        [TestMethod]
        public void BuildReturnsSelectedProperty()
        {
            TestObject.VisitLambda(TestLambdaNode);
            TestObject.VisitMemberAccess(TestMemberAccessNode);

            PropertyInfo actual = TestObject.Build();

            Assert.AreEqual(TestProperty, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuildingWithoutVisitingThrows()
        {
            TestObject.Build();
        }
    }
}
