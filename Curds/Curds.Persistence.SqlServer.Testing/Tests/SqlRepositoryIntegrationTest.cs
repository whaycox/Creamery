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
        private OtherEntity OtherEntity = new OtherEntity();
        private SqlConnectionInformation TestConnectionInformation = new SqlConnectionInformation();
        private string TestServer = "localhost\\SQLEXPRESS";
        private string TestDatabase = "Testing";

        private Mock<IOptions<SqlConnectionInformation>> MockOptions = new Mock<IOptions<SqlConnectionInformation>>();

        private EntityConfiguration<SimpleEntity> SimpleEntityConfiguration = new EntityConfiguration<SimpleEntity>();
        private List<IGlobalConfiguration> TestGlobalConfigurations = new List<IGlobalConfiguration>();
        private List<IEntityConfiguration> TestEntityConfigurations = new List<IEntityConfiguration>();
        private List<IModelConfiguration> TestModelConfigurations = new List<IModelConfiguration>();
        private List<IModelEntityConfiguration> TestModelEntityConfigurations = new List<IModelEntityConfiguration>();
        private ModelConfigurationFactory TestModelConfigurationFactory = null;
        private TypeMapper TestTypeMapper = null;
        private ValueExpressionBuilder TestValueExpressionBuilder = null;
        private DelegateMapper TestDelegateMapper = null;
        private ModelBuilder TestModelBuilder = null;
        private ModelMapFactory TestModelMapFactory = null;
        private ModelMap<ITestDataModel> TestModelMap = null;
        private SqlConnectionStringFactory TestConnectionStringFactory = null;
        private SqlQueryWriterFactory TestQueryWriterFactory = null;
        private SqlConnectionContext TestConnectionContext = null;
        private SqlQueryExpressionParser<ITestDataModel> TestQueryExpressionParser = null;
        private SqlQueryBuilder<ITestDataModel> TestQueryBuilder = null;
        private SqlRepository<ITestDataModel, TestEntity> TestEntityRepository = null;
        private SqlRepository<ITestDataModel, OtherEntity> OtherEntityRepository = null;

        [TestInitialize]
        public void Init()
        {
            SimpleEntityConfiguration = SimpleEntityConfiguration
                .ConfigureColumn(entity => entity.ID)
                .IsIdentity()
                .RegisterColumn();
            TestEntityConfigurations.Add(SimpleEntityConfiguration);
            TestConnectionInformation.Server = TestServer;
            TestConnectionInformation.Database = TestDatabase;

            MockOptions
                .Setup(options => options.Value)
                .Returns(TestConnectionInformation);
        }

        private void BuildTestObjects()
        {
            TestModelConfigurationFactory = new ModelConfigurationFactory(
                TestGlobalConfigurations,
                TestEntityConfigurations,
                TestModelConfigurations,
                TestModelEntityConfigurations);
            TestTypeMapper = new TypeMapper();
            TestValueExpressionBuilder = new ValueExpressionBuilder();
            TestDelegateMapper = new DelegateMapper(
                TestValueExpressionBuilder,
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
            TestEntityRepository = new SqlRepository<ITestDataModel, TestEntity>(
                TestConnectionContext, 
                TestQueryBuilder);
            OtherEntityRepository = new SqlRepository<ITestDataModel, OtherEntity>(
                TestConnectionContext,
                TestQueryBuilder);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestConnectionContext?.Dispose();
        }

        [TestMethod]
        public async Task CanInsertTestEntity()
        {
            BuildTestObjects();

            await TestEntityRepository.Insert(TestEntity);
        }

        [TestMethod]
        public async Task CanInsertOtherEntity()
        {
            BuildTestObjects();

            await OtherEntityRepository.Insert(OtherEntity);
        }

    }
}
