using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Queries.Implementation;

    [TestClass]
    public class SqlUniverseTest
    {
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        private Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        private Mock<ISqlQueryPhraseBuilder> MockPhraseBuilder = new Mock<ISqlQueryPhraseBuilder>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlJoinClause> MockJoinClause = new Mock<ISqlJoinClause>();
        private Mock<ISqlQueryToken> MockFilter = new Mock<ISqlQueryToken>();

        private SqlUniverse<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockQueryContext
                .Setup(context => context.TokenFactory)
                .Returns(MockTokenFactory.Object);
            MockQueryContext
                .Setup(context => context.PhraseBuilder)
                .Returns(MockPhraseBuilder.Object);
            MockQueryContext
                .Setup(context => context.AddTable<TestEntity>())
                .Returns(MockTable.Object);

            TestObject = new SqlUniverse<ITestDataModel, TestEntity>(MockQueryContext.Object);
        }

        [TestMethod]
        public void BuildingUniverseAddsTableForEntity()
        {
            MockQueryContext.Verify(context => context.AddTable<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void TokensBuildsFromTablePhrase()
        {
            TestObject.Tokens.ToList();

            MockPhraseBuilder.Verify(builder => builder.FromTableToken(MockTable.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(14)]
        [DataRow(17)]
        public void TokensBuildsJoinTablePhraseForEachJoinClause(int joinClauses)
        {
            for (int i = 0; i < joinClauses; i++)
                TestObject.JoinCollection.Add(MockJoinClause.Object);

            TestObject.Tokens.ToList();

            MockPhraseBuilder.Verify(builder => builder.JoinTableToken(MockJoinClause.Object), Times.Exactly(joinClauses));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(14)]
        [DataRow(17)]
        public void TokensBuildsFilterPhraseForEachFilter(int filters)
        {
            ISqlQueryToken whereToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.WHERE));
            ISqlQueryToken andToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.AND));
            for (int i = 0; i < filters; i++)
                TestObject.FilterCollection.Add(MockFilter.Object);

            TestObject.Tokens.ToList();

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.WHERE), Times.Once);
            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.AND), Times.Exactly(filters - 1));
            MockTokenFactory.Verify(factory => factory.Phrase(
                whereToken,
                MockFilter.Object), Times.Once);
            MockTokenFactory.Verify(factory => factory.Phrase(
                andToken,
                MockFilter.Object), Times.Exactly(filters - 1));
        }

        [TestMethod]
        public void ProjectEntityIsExpectedType()
        {
            ISqlQuery<TestEntity> actual = TestObject.Project();

            Assert.IsInstanceOfType(actual, typeof(ProjectEntityQuery<ITestDataModel, TestEntity>));
        }

        [TestMethod]
        public void ProjectEntityAttachesSource()
        {
            ISqlQuery actual = TestObject.Project();

            ProjectEntityQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<ITestDataModel, TestEntity>>();
            Assert.AreSame(TestObject, query.Source);
        }

        [TestMethod]
        public void ProjectedTableIsAddedFromContext()
        {
            ISqlQuery actual = TestObject.Project();

            ProjectEntityQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<ITestDataModel, TestEntity>>();
            Assert.AreSame(MockTable.Object, query.ProjectedTable);
        }

        [TestMethod]
        public void DeleteIsExpectedType()
        {
            ISqlQuery actual = TestObject.Delete();

            Assert.IsInstanceOfType(actual, typeof(DeleteEntityQuery<ITestDataModel, TestEntity>));
        }

        [TestMethod]
        public void DeleteAttachesSource()
        {
            ISqlQuery actual = TestObject.Delete();

            DeleteEntityQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<DeleteEntityQuery<ITestDataModel, TestEntity>>();
            Assert.AreSame(TestObject, query.Source);
        }

        [TestMethod]
        public void DeletedTableIsAddedFromContext()
        {
            ISqlQuery actual = TestObject.Project();

            ProjectEntityQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<ITestDataModel, TestEntity>>();
            Assert.AreSame(MockTable.Object, query.ProjectedTable);
        }

        private Expression<Func<TestEntity, bool>> TestWhereExpression => entity => true;

        [TestMethod]
        public void WhereReturnsSameObject()
        {
            ISqlUniverse<ITestDataModel, TestEntity> actual = TestObject.Where(TestWhereExpression);

            Assert.AreSame(TestObject, actual);
        }

        [TestMethod]
        public void WhereParsesExpression()
        {
            var testExpression = TestWhereExpression;

            TestObject.Where(testExpression);

            MockQueryContext.Verify(context => context.ParseQueryExpression(testExpression), Times.Once);
        }

        [TestMethod]
        public void WhereAddsParsedTokenToFilters()
        {
            var testExpression = TestWhereExpression;
            ISqlQueryToken testFilterToken = MockQueryContext.SetupMock(context => context.ParseQueryExpression(It.IsAny<Expression>()));

            TestObject.Where(testExpression);

            CollectionAssert.AreEqual(new[] { testFilterToken }, TestObject.FilterCollection);
        }

        [TestMethod]
        public void UpdateIsExpectedType()
        {
            IEntityUpdate<TestEntity> actual = TestObject.Update();

            Assert.IsInstanceOfType(actual, typeof(EntityUpdateQuery<ITestDataModel, TestEntity>));
        }

        [TestMethod]
        public void UpdateAttachesSource()
        {
            IEntityUpdate<TestEntity> actual = TestObject.Update();

            EntityUpdateQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<EntityUpdateQuery<ITestDataModel, TestEntity>>();
            Assert.AreSame(TestObject, query.Source);
        }

        [TestMethod]
        public void UpdatedTableIsAddedFromContext()
        {
            IEntityUpdate<TestEntity> actual = TestObject.Update();

            EntityUpdateQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<EntityUpdateQuery<ITestDataModel, TestEntity>>();
            Assert.AreSame(MockTable.Object, query.UpdatedTable);
        }

        [TestMethod]
        public void JoinIsExpectedType()
        {
            var actual = TestObject.Join(model => model.Children);

            Assert.IsInstanceOfType(actual, typeof(SqlJoinClause<ITestDataModel, TestEntity, ISqlUniverse<ITestDataModel, TestEntity>, Child>));
        }

        [TestMethod]
        public void AddJoinAddsToCollection()
        {
            var testJoinClause = Mock.Of<ISqlJoinClause<ITestDataModel, TestEntity, ISqlUniverse<ITestDataModel, TestEntity>, Child>>();

            TestObject.AddJoin(testJoinClause);

            CollectionAssert.AreEqual(new[] { testJoinClause }, TestObject.JoinCollection);
        }

        [TestMethod]
        public void AddJoinReturnsExpectedType()
        {
            var testJoinClause = Mock.Of<ISqlJoinClause<ITestDataModel, TestEntity, ISqlUniverse<ITestDataModel, TestEntity>, Child>>();

            var actual = TestObject.AddJoin(testJoinClause);

            Assert.IsInstanceOfType(actual, typeof(JoinedSqlUniverse<ITestDataModel, TestEntity, Child>));
        }
    }
}
