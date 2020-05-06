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

    [TestClass]
    public class SqlUniverseTest
    {
        private Mock<IModelMap> MockModelMap = new Mock<IModelMap>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private SqlUniverse<TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMap
                .Setup(map => map.Entity<TestEntity>())
                .Returns(MockEntityModel.Object);

            TestObject = new SqlUniverse<TestEntity>(MockModelMap.Object);
        }

        [TestMethod]
        public void BuildingUniverseBuildsModelForEntity()
        {
            MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void ProjectEntityIsExpectedType()
        {
            ISqlQuery<TestEntity> actual = TestObject.ProjectEntity();

            Assert.IsInstanceOfType(actual, typeof(ProjectEntityQuery<TestEntity>));
        }

        [TestMethod]
        public void ProjectEntityAttachesSource()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            Assert.AreSame(TestObject, query.Source);
        }

        [TestMethod]
        public void ProjectedTableIsExpectedType()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            Assert.IsInstanceOfType(query.ProjectedTable, typeof(SqlTable));
        }

        [TestMethod]
        public void ProjectedTableHasBuiltModel()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            SqlTable table = query.ProjectedTable.VerifyIsActually<SqlTable>();
            Assert.AreSame(MockEntityModel.Object, table.Model);
        }
    }
}
