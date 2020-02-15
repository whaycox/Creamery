using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class SqlQueryBuilderTest
    {
        private List<TestEntity> TestEntities = new List<TestEntity>();
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity TestValueEntity = new ValueEntity();
        private InsertQuery<TestEntity> TestInsertQuery = new InsertQuery<TestEntity>();

        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();
        private Mock<ISqlQueryExpressionParser<ITestDataModel>> MockQueryExpressionParser = new Mock<ISqlQueryExpressionParser<ITestDataModel>>();

        private SqlQueryBuilder<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMap
                .Setup(map => map.ValueEntity(It.IsAny<TestEntity>()))
                .Returns(TestValueEntity);
            MockQueryExpressionParser
                .Setup(parser => parser.Parse(It.IsAny<Expression<Func<ITestDataModel, ITable<TestEntity>>>>()))
                .Returns(TestInsertQuery);

            TestObject = new SqlQueryBuilder<ITestDataModel>(
                MockModelMap.Object,
                MockQueryExpressionParser.Object);
        }

        [TestMethod]
        public void InsertParsesExpression()
        {
            TestObject.Insert(model => model.Test, TestEntity);

            MockQueryExpressionParser.Verify(parser => parser.Parse(model => model.Test), Times.Once);
        }

        [TestMethod]
        public void InsertMapsValueEntity()
        {
            TestObject.Insert(model => model.Test, TestEntity);

            MockModelMap.Verify(map => map.ValueEntity(TestEntity), Times.Once);
        }

        [TestMethod]
        public void InsertReturnsParsedQuery()
        {
            ISqlQuery actual = TestObject.Insert(model => model.Test, TestEntity);

            Assert.AreSame(TestInsertQuery, actual);
        }

        [TestMethod]
        public void InsertAttachesMappedEntityToParsedQuery()
        {
            TestObject.Insert(model => model.Test, TestEntity);

            Assert.AreSame(TestValueEntity, TestInsertQuery.Entities[0]);
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

            TestObject.Insert(model => model.Test, TestEntities);

            MockModelMap.Verify(map => map.ValueEntity(TestEntity), Times.Exactly(entities));
        }

        [TestMethod]
        public void InsertManyReturnsParsedQuery()
        {
            PopulateNEntities(1);

            ISqlQuery actual = TestObject.Insert(model => model.Test, TestEntities);

            Assert.AreSame(TestInsertQuery, actual);
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

            TestObject.Insert(model => model.Test, TestEntities);

            Assert.AreEqual(entities, TestInsertQuery.Entities.Count);
            Assert.IsTrue(TestInsertQuery.Entities.All(entity => entity == TestValueEntity));
        }
    }
}
