using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Expressions.Abstraction;

    [TestClass]
    public class SqlQueryContextTest
    {
        private Expression TestExpression = Expression.Constant(1);

        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();
        private Mock<IEntityModel> MockModel = new Mock<IEntityModel>();
        private Mock<IExpressionNodeFactory> MockExpressionNodeFactory = new Mock<IExpressionNodeFactory>();
        private Mock<IExpressionNode> MockExpressionNode = new Mock<IExpressionNode>();
        private Mock<ISqlQueryExpressionVisitorFactory> MockExpressionVisitorFactory = new Mock<ISqlQueryExpressionVisitorFactory>();
        private Mock<ISqlQueryTokenVisitor<ITestDataModel>> MockTokenVisitor = new Mock<ISqlQueryTokenVisitor<ITestDataModel>>();
        private Mock<ISqlTableVisitor<ITestDataModel>> MockTableVisitor = new Mock<ISqlTableVisitor<ITestDataModel>>();
        private Mock<ISqlQueryFormatter> MockQueryFormatter = new Mock<ISqlQueryFormatter>();
        private Mock<ISqlConnectionContext> MockConnectionContext = new Mock<ISqlConnectionContext>();
        private Mock<ISqlQueryAliasBuilder> MockAliasBuilder = new Mock<ISqlQueryAliasBuilder>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        private Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        private Mock<ISqlQueryPhraseBuilder> MockPhraseBuilder = new Mock<ISqlQueryPhraseBuilder>();

        private SqlQueryContext<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMap
                .Setup(map => map.Entity<TestEntity>())
                .Returns(MockModel.Object);
            MockExpressionNodeFactory
                .Setup(factory => factory.Build(It.IsAny<Expression>()))
                .Returns(MockExpressionNode.Object);
            MockExpressionVisitorFactory
                .Setup(factory => factory.TokenVisitor(It.IsAny<ISqlQueryContext<ITestDataModel>>()))
                .Returns(MockTokenVisitor.Object);
            MockExpressionVisitorFactory
                .Setup(factory => factory.TableVisitor(It.IsAny<ISqlQueryContext<ITestDataModel>>()))
                .Returns(MockTableVisitor.Object);


            TestObject = new SqlQueryContext<ITestDataModel>(
                MockModelMap.Object,
                MockExpressionNodeFactory.Object,
                MockExpressionVisitorFactory.Object,
                MockQueryFormatter.Object,
                MockConnectionContext.Object,
                MockAliasBuilder.Object,
                MockParameterBuilder.Object,
                MockTokenFactory.Object,
                MockPhraseBuilder.Object);
        }

        [TestMethod]
        public void FormatterIsFromConstructor()
        {
            Assert.AreSame(MockQueryFormatter.Object, TestObject.Formatter);
        }

        [TestMethod]
        public void ConnectionContextIsFromConstructor()
        {
            Assert.AreSame(MockConnectionContext.Object, TestObject.ConnectionContext);
        }

        [TestMethod]
        public void ParameterBuilderIsFromConstructor()
        {
            Assert.AreSame(MockParameterBuilder.Object, TestObject.ParameterBuilder);
        }

        [TestMethod]
        public void TokenFactoryIsFromConstructor()
        {
            Assert.AreSame(MockTokenFactory.Object, TestObject.TokenFactory);
        }

        [TestMethod]
        public void PhraseBuilderIsFromConstructor()
        {
            Assert.AreSame(MockPhraseBuilder.Object, TestObject.PhraseBuilder);
        }

        [TestMethod]
        public void AddTableBuildsModelForEntity()
        {
            TestObject.AddTable<TestEntity>();

            MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void AddTableReturnsExpectedTable()
        {
            ISqlTable actual = TestObject.AddTable<TestEntity>();

            SqlTable actualTable = actual.VerifyIsActually<SqlTable>();
            Assert.AreSame(MockModel.Object, actualTable.Model);
        }

        [TestMethod]
        public void AddTableAddsReturnedToQueryTables()
        {
            ISqlTable actual = TestObject.AddTable<TestEntity>();

            Assert.AreEqual(1, TestObject.Tables.Count);
            Assert.AreSame(actual, TestObject.Tables[0]);
        }

        [TestMethod]
        public void ParseQueryExpressionBuildsNodes()
        {
            TestObject.ParseQueryExpression(TestExpression);

            MockExpressionNodeFactory.Verify(factory => factory.Build(TestExpression), Times.Once);
        }

        [TestMethod]
        public void ParseQueryExpressionBuildsVisitor()
        {
            TestObject.ParseQueryExpression(TestExpression);

            MockExpressionVisitorFactory.Verify(factory => factory.TokenVisitor(TestObject), Times.Once);
        }

        [TestMethod]
        public void ParseQueryExpressionVisitsNode()
        {
            TestObject.ParseQueryExpression(TestExpression);

            MockExpressionNode.Verify(node => node.AcceptVisitor(MockTokenVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void ParseTableExpressionBuildsNodes()
        {
            TestObject.ParseTableExpression(TestExpression);

            MockExpressionNodeFactory.Verify(factory => factory.Build(TestExpression), Times.Once);
        }

        [TestMethod]
        public void ParseTableExpressionBuildsVisitor()
        {
            TestObject.ParseTableExpression(TestExpression);

            MockExpressionVisitorFactory.Verify(factory => factory.TableVisitor(TestObject), Times.Once);
        }

        [TestMethod]
        public void ParseTableExpressionVisitsNode()
        {
            TestObject.ParseTableExpression(TestExpression);

            MockExpressionNode.Verify(node => node.AcceptVisitor(MockTableVisitor.Object), Times.Once);
        }
    }
}
