using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using Whey;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Queries.Implementation;

    [TestClass]
    public class SqlUniverseTest
    {
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private SqlUniverse<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockQueryContext
                .Setup(context => context.AddTable<TestEntity>())
                .Returns(MockTable.Object);

            TestObject = new SqlUniverse<ITestDataModel, TestEntity>(MockQueryContext.Object);
        }

        [TestMethod]
        public void BuildingUniverseAddsTableForEntity()
        {
            MockQueryContext.Verify(context => context.AddTable<TestEntity>());
        }

        [TestMethod]
        public void TablesReturnsFromQueryContext()
        {
            MockQueryContext
                .Setup(context => context.Tables)
                .Returns(new List<ISqlTable> { MockTable.Object });

            throw new NotImplementedException();
            //CollectionAssert.AreEqual(new[] { MockTable.Object }, TestObject.Tables.ToList());
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
            ISqlQueryToken testFilterToken = Mock.Of<ISqlQueryToken>();
            MockQueryContext
                .Setup(context => context.ParseQueryExpression(It.IsAny<Expression>()))
                .Returns(testFilterToken);

            TestObject.Where(testExpression);

            throw new NotImplementedException();
            //CollectionAssert.AreEqual(new[] { testFilterToken }, TestObject.Filters.ToList());
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
    }
}
