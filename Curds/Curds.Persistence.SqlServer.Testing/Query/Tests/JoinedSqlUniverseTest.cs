using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Queries.Implementation;

    [TestClass]
    public class JoinedSqlUniverseTest
    {
        private SqlUniverse<ITestDataModel, Parent> TestSourceUniverse = null;
        private ISqlTable TestTable = Mock.Of<ISqlTable>();
        private ISqlQueryToken TestFilterToken = Mock.Of<ISqlQueryToken>();

        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();

        private JoinedSqlUniverse<ITestDataModel, Parent, Child> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestSourceUniverse = new SqlUniverse<ITestDataModel, Parent>(MockQueryContext.Object);

            MockQueryContext
                .Setup(context => context.ParseTableExpression(It.IsAny<Expression>()))
                .Returns(TestTable);
            MockQueryContext
                .Setup(context => context.ParseQueryExpression(It.IsAny<Expression>()))
                .Returns(TestFilterToken);

            TestObject = new JoinedSqlUniverse<ITestDataModel, Parent, Child>(TestSourceUniverse);
        }

        [TestMethod]
        public void QueryContextIsFromSource()
        {
            Assert.AreSame(MockQueryContext.Object, TestObject.QueryContext);
        }

        [TestMethod]
        public void TokensAreFromSource()
        {
            ISqlQueryToken expectedToken = MockQueryContext.SetupMock(context => context.PhraseBuilder.FromTableToken(It.IsAny<ISqlTable>()));

            IEnumerable<ISqlQueryToken> actual = TestObject.Tokens;

            CollectionAssert.AreEqual(new[] { expectedToken }, actual.ToList());
        }

        private Expression<Func<Parent, Child, Parent>> TestProjectionExpression = (parent, child) => parent;

        [TestMethod]
        public void ProjectParsesProvidedProjectionExpression()
        {
            TestObject.Project(TestProjectionExpression);

            MockQueryContext.Verify(context => context.ParseTableExpression(TestProjectionExpression), Times.Once);
        }

        [TestMethod]
        public void ProjectBuildsExpectedType()
        {
            ISqlQuery<Parent> actual = TestObject.Project(TestProjectionExpression);

            ProjectEntityQuery<ITestDataModel, Parent> actualQuery = actual.VerifyIsActually<ProjectEntityQuery<ITestDataModel, Parent>>();
            Assert.AreSame(TestObject, actualQuery.Source);
            Assert.AreSame(TestTable, actualQuery.ProjectedTable);
        }

        private Expression<Func<Parent, Child, bool>> TestFilterExpression = (parent, child) => parent.ID == child.ID;

        [TestMethod]
        public void WhereParsesFilterExpression()
        {
            TestObject.Where(TestFilterExpression);

            MockQueryContext.Verify(context => context.ParseQueryExpression(TestFilterExpression), Times.Once);
        }

        [TestMethod]
        public void WhereAddsParsedFilterTokenToSourceCollection()
        {
            TestObject.Where(TestFilterExpression);

            CollectionAssert.AreEqual(new[] { TestFilterToken }, TestSourceUniverse.FilterCollection);
        }

        [TestMethod]
        public void WhereReturnsSameObject()
        {
            ISqlUniverse<ITestDataModel, Parent, Child> actual = TestObject.Where(TestFilterExpression);

            Assert.AreSame(TestObject, actual);
        }
    }
}
