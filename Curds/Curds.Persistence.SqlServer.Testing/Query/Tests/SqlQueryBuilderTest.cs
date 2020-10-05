using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Model.Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Queries.Implementation;

    [TestClass]
    public class SqlQueryBuilderTest
    {
        private List<TestEntity> TestEntities = new List<TestEntity>();
        private TestEntity TestEntity = new TestEntity();

        private Mock<IServiceProvider> MockServiceProvider = new Mock<IServiceProvider>();
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();

        private SqlQueryBuilder<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockServiceProvider
                .Setup(provider => provider.GetService(typeof(ISqlQueryContext<ITestDataModel>)))
                .Returns(MockQueryContext.Object);
            MockServiceProvider
                .Setup(provider => provider.GetService(typeof(IModelMap<ITestDataModel>)))
                .Returns(MockModelMap.Object);

            TestObject = new SqlQueryBuilder<ITestDataModel>(MockServiceProvider.Object);
        }

        [TestMethod]
        public void InsertRequestsQueryContext()
        {
            TestObject.Insert(TestEntities);

            MockServiceProvider.Verify(provider => provider.GetService(typeof(ISqlQueryContext<ITestDataModel>)), Times.Once);
        }

        [TestMethod]
        public void InsertReturnsExpectedType()
        {
            ISqlQuery actual = TestObject.Insert(TestEntities);

            Assert.IsInstanceOfType(actual, typeof(InsertQuery<ITestDataModel, TestEntity>));
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
        public void FromRequestsQueryContext()
        {
            TestObject.From<TestEntity>();

            MockServiceProvider.Verify(provider => provider.GetService(typeof(ISqlQueryContext<ITestDataModel>)), Times.Once);
        }

        [TestMethod]
        public void FromReturnsExpectedType()
        {
            ISqlUniverse<ITestDataModel, TestEntity> actual = TestObject.From<TestEntity>();

            Assert.IsInstanceOfType(actual, typeof(SqlUniverse<ITestDataModel, TestEntity>));
        }
    }
}
