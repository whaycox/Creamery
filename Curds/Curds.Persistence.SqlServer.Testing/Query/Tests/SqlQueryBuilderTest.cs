using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Model.Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class SqlQueryBuilderTest
    {
        //private List<TestEntity> TestEntities = new List<TestEntity>();
        //private TestEntity TestEntity = new TestEntity();
        //private ValueEntity TestValueEntity = new ValueEntity();
        //private Table TestTable = new Table();

        //private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();
        //private Mock<IEntityModel<TestEntity>> MockEntityModel = new Mock<IEntityModel<TestEntity>>();
        //private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();

        private SqlQueryBuilder<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
            //MockModelMap
            //    .Setup(map => map.Entity<TestEntity>())
            //    .Returns(MockEntityModel.Object);

            //TestObject = new SqlQueryBuilder<ITestDataModel>(MockModelMap.Object);
        }

        [TestMethod]
        public void InsertReturnsExpectedType()
        {
            throw new NotImplementedException();
            //ISqlQuery actual = TestObject.Insert(TestEntities);

            //Assert.IsInstanceOfType(actual, typeof(InsertQuery<TestEntity>));
        }

        [TestMethod]
        public void InsertBuildsModel()
        {
            throw new NotImplementedException();
            //TestObject.Insert(TestEntities);

            //MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void InsertSetsModel()
        {
            throw new NotImplementedException();
            //ISqlQuery actual = TestObject.Insert(TestEntities);

            //InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            //Assert.AreSame(MockEntityModel.Object, testQuery.Model);
        }

        private void PopulateNEntities(int entities)
        {
            //for (int i = 0; i < entities; i++)
            //    TestEntities.Add(TestEntity);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void InsertSetsEntities(int entities)
        {
            throw new NotImplementedException();
            //PopulateNEntities(entities);

            //ISqlQuery actual = TestObject.Insert(TestEntities);

            //InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            //CollectionAssert.AreEqual(TestEntities, testQuery.Entities);
        }

        [TestMethod]
        public void FromReturnsExpectedType()
        {
            ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            Assert.IsInstanceOfType(actual, typeof(SqlUniverse<TestEntity>));
        }

        [TestMethod]
        public void FromBuildsModel()
        {
            throw new NotImplementedException();
            //TestObject.From<TestEntity>();

            //MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void FromSetsModel()
        {
            throw new NotImplementedException();
            //ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            //SqlUniverse<TestEntity> universe = actual.VerifyIsActually<SqlUniverse<TestEntity>>();
            //Assert.AreSame(MockEntityModel.Object, universe.Model);
        }
    }
}
