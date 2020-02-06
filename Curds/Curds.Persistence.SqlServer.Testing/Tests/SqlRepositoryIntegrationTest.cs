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
using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;
    using Model.Abstraction;
    using Query.Implementation;
    using Model.Implementation;
    using Model.Configuration.Implementation;
    using Model.Configuration.Abstraction;
    using Model.Configuration.Domain;

    [TestClass]
    public class SqlRepositoryIntegrationTest
    {
        private TestEntity TestEntity = new TestEntity();
        private SqlConnectionInformation TestConnectionInformation = new SqlConnectionInformation();
        private string TestServer = "localhost\\SQLEXPRESS";
        private string TestDatabase = "Testing";

        private Mock<IOptions<SqlConnectionInformation>> MockOptions = new Mock<IOptions<SqlConnectionInformation>>();

        private EntityConfiguration<SimpleEntity> SimpleEntityConfiguration = new EntityConfiguration<SimpleEntity> { Identity = nameof(SimpleEntity.ID) };
        private List<IGlobalConfiguration> TestGlobalConfigurations = new List<IGlobalConfiguration>();
        private List<IEntityConfiguration> TestEntityConfigurations = new List<IEntityConfiguration>();
        private List<IModelConfiguration> TestModelConfigurations = new List<IModelConfiguration>();
        private List<IModelEntityConfiguration> TestModelEntityConfigurations = new List<IModelEntityConfiguration>();
        private ModelConfigurationFactory TestModelConfigurationFactory = null;
        private TypeMapper TestTypeMapper = null;
        private DelegateMapper TestDelegateMapper = null;
        private ModelBuilder TestModelBuilder = null;
        private ModelMapFactory TestModelMapFactory = null;
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
            TestEntityConfigurations.Add(SimpleEntityConfiguration);
            TestConnectionInformation.Server = TestServer;
            TestConnectionInformation.Database = TestDatabase;

            MockOptions
                .Setup(options => options.Value)
                .Returns(TestConnectionInformation);
        }

        private void BuildTestRepository()
        {
            TestModelConfigurationFactory = new ModelConfigurationFactory(
                TestGlobalConfigurations,
                TestEntityConfigurations,
                TestModelConfigurations,
                TestModelEntityConfigurations);
            TestTypeMapper = new TypeMapper();
            TestDelegateMapper = new DelegateMapper(
                TestTypeMapper,
                TestModelConfigurationFactory);
            TestModelBuilder = new ModelBuilder(
                TestModelConfigurationFactory,
                TestTypeMapper, 
                TestDelegateMapper);
            TestModelMapFactory = new ModelMapFactory(TestModelBuilder);
            TestModelMap = TestModelMapFactory.Build<ITestDataModel>() as ModelMap<ITestDataModel>;
            TestConnectionStringFactory = new SqlConnectionStringFactory();
            TestQueryWriterFactory = new SqlQueryWriterFactory();
            TestConnectionContext = new SqlConnectionContext(
                TestConnectionStringFactory,
                MockOptions.Object,
                TestQueryWriterFactory);
            TestQueryExpressionParser = new SqlQueryExpressionParser<ITestDataModel>(TestModelMap);
            TestQueryBuilder = new SqlQueryBuilder<ITestDataModel>(
                TestModelMap, 
                TestQueryExpressionParser);
            TestRepository = new SqlRepository<ITestDataModel, TestEntity>(
                TestConnectionContext, 
                TestQueryBuilder);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestConnectionContext?.Dispose();
        }

        [TestMethod]
        public async Task CanInsertNewEntity()
        {
            BuildTestRepository();

            await TestRepository.Insert(TestEntity);
        }

    }
}
