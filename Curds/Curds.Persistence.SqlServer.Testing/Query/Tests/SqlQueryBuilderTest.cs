using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Whey;
using System;
using System.Linq;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Queries.Implementation;

    [TestClass]
    public class SqlQueryBuilderTest
    {
        private List<TestEntity> TestEntities = new List<TestEntity>();
        private TestEntity TestEntity = new TestEntity();

        private Mock<IServiceProvider> MockServiceProvider = new Mock<IServiceProvider>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private SqlQueryBuilder<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlQueryBuilder<ITestDataModel>(MockServiceProvider.Object);
        }

        [TestMethod]
        public void InsertReturnsExpectedType()
        {
            ISqlQuery actual = TestObject.Insert(TestEntities);

            Assert.IsInstanceOfType(actual, typeof(InsertQuery<ITestDataModel, TestEntity>));
        }

        [TestMethod]
        public void InsertBuildsModel()
        {
            TestObject.Insert(TestEntities);

            throw new NotImplementedException();
            //MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void InsertSetsTableModelToBuiltResult()
        {
            throw new NotImplementedException();
            //ISqlQuery actual = TestObject.Insert(TestEntities);

            //InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            //SqlTable actualTable = testQuery.Table.VerifyIsActually<SqlTable>();
            //Assert.AreSame(MockEntityModel.Object, actualTable.Model);
        }

        private void PopulateNEntities(int entities)
        {
            for (int i = 0; i < entities; i++)
                TestEntities.Add(TestEntity);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void InsertSetsEntities(int entities)
        {
            PopulateNEntities(entities);

            ISqlQuery actual = TestObject.Insert(TestEntities);

            InsertQuery<ITestDataModel, TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<ITestDataModel, TestEntity>>();
            CollectionAssert.AreEqual(TestEntities, testQuery.Entities);
        }

        [TestMethod]
        public void FromReturnsExpectedType()
        {
            ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            Assert.IsInstanceOfType(actual, typeof(SqlUniverse<ITestDataModel, TestEntity>));
        }

        [TestMethod]
        public void FromBuildsModel()
        {
            TestObject.From<TestEntity>();

            throw new NotImplementedException();
            //MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }
    }
}
