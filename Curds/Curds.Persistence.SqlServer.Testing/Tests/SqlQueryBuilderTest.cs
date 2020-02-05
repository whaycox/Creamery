using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;
    using Query.Implementation;
    using Model.Abstraction;
    using Query.Domain;

    [TestClass]
    public class SqlQueryBuilderTest
    {
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

            Assert.AreSame(TestValueEntity, TestInsertQuery.Entity);
        }
    }
}
