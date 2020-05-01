using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class SqlUniverseTest
    {
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private SqlUniverse<TestEntity> TestObject = new SqlUniverse<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            TestObject.Model = MockEntityModel.Object;
        }

        [TestMethod]
        public void ProjectEntityIsExpectedType()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            Assert.IsInstanceOfType(actual, typeof(ProjectEntityQuery<TestEntity>));
        }

        [TestMethod]
        public void ProjectEntityAttachesModel()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            Assert.AreSame(MockEntityModel.Object, query.Model);
        }
    }
}
