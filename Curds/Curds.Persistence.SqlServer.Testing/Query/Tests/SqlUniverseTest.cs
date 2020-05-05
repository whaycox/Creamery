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
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private SqlUniverse TestObject = null;

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ProjectEntityIsExpectedType()
        {
            throw new NotImplementedException();
            //ISqlQuery<TestEntity> actual = TestObject.ProjectEntity();

            //Assert.IsInstanceOfType(actual, typeof(ProjectEntityQuery<TestEntity>));
        }

        [TestMethod]
        public void ProjectEntityAttachesModel()
        {
            throw new NotImplementedException();
            //ISqlQuery actual = TestObject.ProjectEntity();

            //ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            //Assert.AreSame(MockEntityModel.Object, query.Model);
        }
    }
}
