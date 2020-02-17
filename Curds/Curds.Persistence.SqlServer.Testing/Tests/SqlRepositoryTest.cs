using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Query.Abstraction;
    using Model.Abstraction;

    [TestClass]
    public class SqlRepositoryTest
    {
        private TestEntity TestEntity = new TestEntity();

        private Mock<ISqlConnectionContext> MockConnectionContext = new Mock<ISqlConnectionContext>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<IModelMap<ITestDataModel>> MockModelMap = new Mock<IModelMap<ITestDataModel>>();
        private Mock<ISqlQueryBuilder<ITestDataModel>> MockQueryBuilder = new Mock<ISqlQueryBuilder<ITestDataModel>>();
        private Mock<ISqlQuery> MockQuery = new Mock<ISqlQuery>();

        private SqlRepository<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockConnectionContext
                .Setup(context => context.ExecuteWithResult(It.IsAny<ISqlQuery>()))
                .ReturnsAsync(MockQueryReader.Object);
            MockQueryBuilder
                .Setup(builder => builder.Insert(It.IsAny<Expression<Func<ITestDataModel, ITable<TestEntity>>>>(), It.IsAny<IEnumerable<TestEntity>>()))
                .Returns(MockQuery.Object);

            TestObject = new SqlRepository<ITestDataModel, TestEntity>(
                MockConnectionContext.Object,
                MockModelMap.Object,
                MockQueryBuilder.Object);
        }

        [TestMethod]
        public async Task InsertBuildsAppropriateQuery()
        {
            await TestObject.Insert(TestEntity);

            MockQueryBuilder.Verify(builder => builder.Insert(model => model.Table<TestEntity>(), It.Is<IEnumerable<TestEntity>>(entities => 
                entities.Count() == 1 &&
                entities.First() == TestEntity)), Times.Once);
        }

        [TestMethod]
        public async Task InsertExecutesWithResultBuiltQuery()
        {
            await TestObject.Insert(TestEntity);

            MockConnectionContext.Verify(context => context.ExecuteWithResult(MockQuery.Object), Times.Once);
        }

        [TestMethod]
        public async Task InsertProcessesResultWithBuiltQuery()
        {
            await TestObject.Insert(TestEntity);

            MockQuery.Verify(query => query.ProcessResult(MockQueryReader.Object), Times.Once);
        }

    }
}
