using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using ExpressionNodes.Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class TableExpressionVisitorTest
    {
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private TableExpressionVisitor<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new TableExpressionVisitor<ITestDataModel>(MockQueryContext.Object);
        }

        [TestMethod]
        public void SetsQueryContext()
        {
            Assert.AreSame(MockQueryContext.Object, TestObject.Context);
        }

        [TestMethod]
        public void IdentifiesTableByEntityType()
        {
            MockTable
                .Setup(table => table.EntityType)
                .Returns(typeof(TestEntity));
            MockQueryContext
                .Setup(context => context.Tables)
                .Returns(new List<ISqlTable> { MockTable.Object });
            ParameterNode testNode = new ParameterNode(Expression.Parameter(typeof(TestEntity), nameof(testNode)));

            ISqlTable actual = TestObject.VisitParameter(testNode);

            Assert.AreSame(MockTable.Object, actual);
        }
    }
}
