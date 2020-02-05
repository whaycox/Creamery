using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;
    using Query.Domain;
    using Query.Implementation;
    using Model.Abstraction;

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
        public void ParseInsertExpressionWithPropertyLooksupTableByName()
        {
            TestObject.Parse(model => model.Test);

            MockModelMap.Verify(map => map.Table(nameof(ITestDataModel.Test)), Times.Once);
        }

        [TestMethod]
        public void ParseInsertExpressionWithPropertyAttachesTable()
        {
            MockModelMap
                .Setup(map => map.Table(It.IsAny<string>()))
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
