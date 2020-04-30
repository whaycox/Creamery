using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Model.Domain;
    using Abstraction;
    using Model.Abstraction;

    [TestClass]
    public class SqlUniverseTest
    {
        //private Table TestTable = new Table();

        //private Mock<IEntityModel<TestEntity>> MockEntityModel = new Mock<IEntityModel<TestEntity>>();

        private SqlUniverse<TestEntity> TestObject = new SqlUniverse<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
            //TestObject.Model = MockEntityModel.Object;
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
            throw new NotImplementedException();
            //ISqlQuery actual = TestObject.ProjectEntity();

            //ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            //Assert.AreSame(MockEntityModel.Object, query.Model);
        }
    }
}
