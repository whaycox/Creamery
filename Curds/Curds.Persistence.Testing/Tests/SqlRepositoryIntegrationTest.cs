using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;
    using Query.Abstraction;
    using Query.Implementation;

    [TestClass]
    public class SqlRepositoryIntegrationTest
    {
        private TestEntity TestEntity = new TestEntity();
        private SqlConnectionInformation TestConnectionInformation = new SqlConnectionInformation();
        private string TestServer = "localhost\\SQLEXPRESS";
        private string TestDatabase = "Testing";

        private Mock<IOptions<SqlConnectionInformation>> MockOptions = new Mock<IOptions<SqlConnectionInformation>>();

        private ModelMapper TestModelMapper = new ModelMapper();
        private ModelMap<ITestDataModel> TestModelMap = null;
        private SqlConnectionStringFactory TestConnectionStringFactory = null;
        private SqlQueryWriterFactory TestQueryWriterFactory = null;
        private SqlConnectionContext TestConnectionContext = null;
        private SqlQueryExpressionParser<ITestDataModel> TestQueryExpressionParser = null;
        private SqlQueryBuilder<ITestDataModel> TestQueryBuilder = null;
        private SqlRepository<ITestDataModel, TestEntity> TestRepository = null;

        [TestInitialize]
        public void Init()
        {
            TestConnectionInformation.Server = TestServer;
            TestConnectionInformation.Database = TestDatabase;

            MockOptions
                .Setup(options => options.Value)
                .Returns(TestConnectionInformation);

            TestModelMap = new ModelMap<ITestDataModel>(TestModelMapper);
            TestConnectionStringFactory = new SqlConnectionStringFactory();
            TestQueryWriterFactory = new SqlQueryWriterFactory();
            TestConnectionContext = new SqlConnectionContext(
                TestConnectionStringFactory, 
                MockOptions.Object,
                TestQueryWriterFactory);
            TestQueryExpressionParser = new SqlQueryExpressionParser<ITestDataModel>(TestModelMap);
            TestQueryBuilder = new SqlQueryBuilder<ITestDataModel>(TestModelMap, TestQueryExpressionParser);
            TestRepository = new SqlRepository<ITestDataModel, TestEntity>(TestConnectionContext, TestQueryBuilder);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestConnectionContext.Dispose();
        }

        [TestMethod]
        public async Task CanInsertNewEntity()
        {
            await TestRepository.Insert(TestEntity);
        }

    }
}
