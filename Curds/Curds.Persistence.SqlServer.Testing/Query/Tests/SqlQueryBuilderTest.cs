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
        private List<TestEntity> TestEntities = new List<TestEntity>();
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity<TestEntity> TestValueEntity = new ValueEntity<TestEntity>();
        private Table TestTable = new Table();

        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();

        private SqlQueryBuilder<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMap
                .Setup(map => map.ValueEntity(It.IsAny<TestEntity>()))
                .Returns(TestValueEntity);
            MockModelMap
                .Setup(map => map.Table(It.IsAny<Type>()))
                .Returns(TestTable);
            MockModelMap
                .Setup(map => map.AssignIdentityDelegate<TestEntity>())
                .Returns(MockAssignIdentityDelegate.Object);

            TestObject = new SqlQueryBuilder<ITestDataModel>(MockModelMap.Object);
        }

        [TestMethod]
        public void InsertReturnsExpectedType()
        {
            ISqlQuery actual = TestObject.Insert(TestEntities);

            actual.VerifyIsActually<InsertQuery<TestEntity>>();
        }

        [TestMethod]
        public void InsertManySetsTable()
        {
            ISqlQuery actual = TestObject.Insert(TestEntities);

            MockModelMap.Verify(map => map.Table(typeof(TestEntity)), Times.Once);
            InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            Assert.AreSame(testQuery.Table, TestTable);
        }

        [TestMethod]
        public void InsertManySetsAssignIdentityDelegate()
        {
            ISqlQuery actual = TestObject.Insert(TestEntities);

            MockModelMap.Verify(map => map.AssignIdentityDelegate<TestEntity>());
            InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            Assert.AreSame(testQuery.AssignIdentityDelegate, MockAssignIdentityDelegate.Object);
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
        public void InsertManyMapsValueEntities(int entities)
        {
            PopulateNEntities(entities);

            TestObject.Insert(TestEntities);

            MockModelMap.Verify(map => map.ValueEntity(TestEntity), Times.Exactly(entities));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void InsertManyAttachesEachToParsedQuery(int entities)
        {
            PopulateNEntities(entities);

            ISqlQuery actual = TestObject.Insert(TestEntities);

            InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            Assert.AreEqual(entities, testQuery.Entities.Count);
            Assert.IsTrue(testQuery.Entities.All(entity => entity == TestValueEntity));
        }

        [TestMethod]
        public void FromReturnsExpectedType()
        {
            ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            actual.VerifyIsActually<SqlUniverse<TestEntity>>();
        }

        [TestMethod]
        public void FromSetsTable()
        {
            ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            MockModelMap.Verify(map => map.Table(typeof(TestEntity)), Times.Once);
            SqlUniverse<TestEntity> universe = actual.VerifyIsActually<SqlUniverse<TestEntity>>();
            Assert.AreSame(TestTable, universe.Table);
        }
    }
}
