﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Whey;
using System;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class SqlQueryBuilderTest
    {
        private List<TestEntity> TestEntities = new List<TestEntity>();
        private TestEntity TestEntity = new TestEntity();

        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private SqlQueryBuilder<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMap
                .Setup(map => map.Entity<TestEntity>())
                .Returns(MockEntityModel.Object);

            TestObject = new SqlQueryBuilder<ITestDataModel>(MockModelMap.Object);
        }

        [TestMethod]
        public void InsertReturnsExpectedType()
        {
            ISqlQuery actual = TestObject.Insert(TestEntities);

            Assert.IsInstanceOfType(actual, typeof(InsertQuery<TestEntity>));
        }

        [TestMethod]
        public void InsertBuildsModel()
        {
            TestObject.Insert(TestEntities);

            MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void InsertSetsModelToBuiltResult()
        {
            throw new NotImplementedException();
            //ISqlQuery actual = TestObject.Insert(TestEntities);

            //InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            //Assert.AreSame(MockEntityModel.Object, testQuery.Model);
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

            InsertQuery<TestEntity> testQuery = actual.VerifyIsActually<InsertQuery<TestEntity>>();
            CollectionAssert.AreEqual(TestEntities, testQuery.Entities);
        }

        [TestMethod]
        public void FromReturnsExpectedType()
        {
            ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            throw new NotImplementedException();
            //Assert.IsInstanceOfType(actual, typeof(SqlUniverse<TestEntity>));
        }

        [TestMethod]
        public void FromBuildsModel()
        {
            TestObject.From<TestEntity>();

            MockModelMap.Verify(map => map.Entity<TestEntity>(), Times.Once);
        }

        [TestMethod]
        public void FromSetsModelToBuiltResult()
        {
            ISqlUniverse<TestEntity> actual = TestObject.From<TestEntity>();

            throw new NotImplementedException();
            //SqlUniverse<TestEntity> universe = actual.VerifyIsActually<SqlUniverse<TestEntity>>();
            //Assert.AreSame(MockEntityModel.Object, universe.Model);
        }
    }
}
