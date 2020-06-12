using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Whey;
using System;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Domain;
    using Queries.Implementation;
    using Persistence.Abstraction;

    [TestClass]
    public class SqlUniverseTest
    {
        private Mock<IModelMap> MockModelMap = new Mock<IModelMap>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private SqlUniverse<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMap
                .Setup(map => map.Entity<TestEntity>())
                .Returns(MockEntityModel.Object);

            throw new NotImplementedException();
            //TestObject = new SqlUniverse<TestEntity>(
            //    MockModelMap.Object,
            //    MockExpressionParser.Object);
        }

        [TestMethod]
        public void BuildingUniverseBuildsModelForEntity()
        {
            MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
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
        public void ProjectedTableIsExpectedType()
        {
            ISqlQuery actual = TestObject.Project();

            ProjectEntityQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<ITestDataModel, TestEntity>>();
            Assert.IsInstanceOfType(query.ProjectedTable, typeof(SqlTable));
        }

        [TestMethod]
        public void ProjectedTableHasBuiltModel()
        {
            ISqlQuery actual = TestObject.Project();

            ProjectEntityQuery<ITestDataModel, TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<ITestDataModel, TestEntity>>();
            SqlTable table = query.ProjectedTable.VerifyIsActually<SqlTable>();
            Assert.AreSame(MockEntityModel.Object, table.Model);
        }
    }
}
