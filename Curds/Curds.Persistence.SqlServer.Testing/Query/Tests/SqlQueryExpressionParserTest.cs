using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Persistence.Query.Tests
{
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Model.Domain;

    [TestClass]
    public class SqlQueryExpressionParserTest
    {
        private Table TestTable = new Table();

        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();

        private SqlQueryExpressionParser<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlQueryExpressionParser<ITestDataModel>(MockModelMap.Object);
        }

        [TestMethod]
        public void ParseInsertExpressionWithPropertyLooksupTableByType()
        {
            TestObject.Parse(model => model.Test);

            MockModelMap.Verify(map => map.Table(typeof(TestEntity)), Times.Once);
        }

        [TestMethod]
        public void ParseInsertExpressionWithPropertyAttachesTable()
        {
            MockModelMap
                .Setup(map => map.Table(It.IsAny<Type>()))
                .Returns(TestTable);

            InsertQuery<TestEntity> actual = TestObject.Parse(model => model.Test);

            Assert.AreSame(TestTable, actual.Table);
        }

        [TestMethod]
        public void ParseInsertExpressionWithMethodLooksupTableByType()
        {
            TestObject.Parse(model => model.Table<TestEntity>());

            MockModelMap.Verify(map => map.Table(typeof(TestEntity)), Times.Once);
        }

        [TestMethod]
        public void ParseInsertExpressionWithMethodAttachesTable()
        {
            MockModelMap
                .Setup(map => map.Table(It.IsAny<Type>()))
                .Returns(TestTable);

            InsertQuery<TestEntity> actual = TestObject.Parse(model => model.Table<TestEntity>());

            Assert.AreSame(TestTable, actual.Table);
        }
    }
}
