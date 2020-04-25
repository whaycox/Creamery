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

    [TestClass]
    public class SqlUniverseTest
    {
        private Table TestTable = new Table();

        private SqlUniverse<TestEntity> TestObject = new SqlUniverse<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            TestObject.Table = TestTable;
        }

        [TestMethod]
        public void ProjectEntityIsExpectedType()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
        }

        [TestMethod]
        public void ProjectEntityAttachesProjectedTable()
        {
            ISqlQuery actual = TestObject.ProjectEntity();

            ProjectEntityQuery<TestEntity> query = actual.VerifyIsActually<ProjectEntityQuery<TestEntity>>();
            Assert.AreSame(TestTable, query.ProjectedTable);
        }
    }
}
