using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Expressions.Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Template;
    using Expressions.Nodes.Domain;

    [TestClass]
    public class TableExpressionVisitorTest : BaseQueryExpressionVisitorTemplate
    {
        private Mock<IExpressionNodeFactory> MockNodeFactory = new Mock<IExpressionNodeFactory>();
        private Mock<IExpressionNode> MockExpressionNode = new Mock<IExpressionNode>();
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private TableExpressionVisitor<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockNodeFactory
                .Setup(factory => factory.Build(It.IsAny<Expression>()))
                .Returns(MockExpressionNode.Object);

            TestObject = new TableExpressionVisitor<ITestDataModel>(MockQueryContext.Object);
        }

        [TestMethod]
        public void SetsQueryContext()
        {
            Assert.AreSame(MockQueryContext.Object, TestObject.Context);
        }

        [TestMethod]
        public void LambdaForwardsBody()
        {
            LambdaExpression testExpression = Expression.Lambda(TestExpressionOne, TestParameterExpression);
            LambdaNode testNode = new LambdaNode(
                MockNodeFactory.Object,
                testExpression);

            TestObject.VisitLambda(testNode);

            MockExpressionNode.Verify(node => node.AcceptVisitor(TestObject), Times.Once);
        }

        [TestMethod]
        public void ParameterIdentifiesTableByEntityType()
        {
            MockTable
                .Setup(table => table.EntityType)
                .Returns(typeof(TestEntity));
            MockQueryContext
                .Setup(context => context.Tables)
                .Returns(new List<ISqlTable> { MockTable.Object });
            ParameterNode testNode = new ParameterNode(TestParameterExpression);


            throw new System.NotImplementedException();
            //ISqlTable actual = TestObject.VisitParameter(testNode);

            //Assert.AreSame(MockTable.Object, actual);
        }
    }
}
