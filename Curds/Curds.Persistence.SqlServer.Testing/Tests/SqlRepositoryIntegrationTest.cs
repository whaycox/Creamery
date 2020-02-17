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
    using Template;

    [TestClass]
    public class SqlRepositoryIntegrationTest : SqlTemplate
    {
        private TestEntity TestEntity = new TestEntity();
        private OtherEntity OtherEntity = new OtherEntity();
        private string TestSchema = nameof(TestSchema);

        private Mock<IOptions<SqlConnectionInformation>> MockOptions = new Mock<IOptions<SqlConnectionInformation>>();

        private EntityConfiguration<SimpleEntity> SimpleEntityConfiguration = new EntityConfiguration<SimpleEntity>();
        private List<IGlobalConfiguration> TestGlobalConfigurations = new List<IGlobalConfiguration>();
        private List<IEntityConfiguration> TestEntityConfigurations = new List<IEntityConfiguration>();
        private List<IModelConfiguration> TestModelConfigurations = new List<IModelConfiguration>();
        private List<IModelEntityConfiguration> TestModelEntityConfigurations = new List<IModelEntityConfiguration>();
        private ModelConfigurationFactory TestModelConfigurationFactory = null;
        private TypeMapper TestTypeMapper = null;
        private ValueExpressionBuilder TestValueExpressionBuilder = null;
        private AssignIdentityExpressionBuilder TestAssignIdentityExpressionBuilder = null;
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

        private void FullyPopulateOtherEntity()
        {
            OtherEntity.NullableBoolValue = OtherEntity.BoolValue;
            OtherEntity.NullableByteValue = OtherEntity.ByteValue;
            OtherEntity.NullableShortValue = OtherEntity.ShortValue;
            OtherEntity.NullableIntValue = OtherEntity.IntValue;
            OtherEntity.NullableLongValue = OtherEntity.LongValue;
            OtherEntity.NullableDateTimeValue = OtherEntity.DateTimeValue;
            OtherEntity.NullableDateTimeOffsetValue = OtherEntity.DateTimeOffsetValue;
            OtherEntity.NullableDecimalValue = OtherEntity.DecimalValue;
            OtherEntity.NullableDoubleValue = OtherEntity.DoubleValue;
        }

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
            TestAssignIdentityExpressionBuilder = new AssignIdentityExpressionBuilder();
            TestDelegateMapper = new DelegateMapper(
                TestValueExpressionBuilder,
                TestAssignIdentityExpressionBuilder,
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
                TestModelMap,
                TestQueryBuilder);
            OtherEntityRepository = new SqlRepository<ITestDataModel, OtherEntity>(
                TestConnectionContext,
                TestModelMap,
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

        [TestMethod]
        public async Task CanInsertPopulatedOtherEntity()
        {
            FullyPopulateOtherEntity();
            BuildTestObjects();

            await OtherEntityRepository.Insert(OtherEntity);
        }

        private void ConfigureCustomTestEntity()
        {
            GlobalConfiguration schemaConfig = new GlobalConfiguration { Schema = TestSchema };
            TestGlobalConfigurations.Add(schemaConfig);
            EntityConfiguration<TestEntity> customConfig = new EntityConfiguration<TestEntity>()
                .WithTableName("TestCustomEntity")
                .ConfigureColumn(entity => entity.ID)
                    .WithColumnName("CustomIdentityField")
                    .RegisterColumn()
                .ConfigureColumn(entity => entity.Name)
                    .WithColumnName("SomeOtherName")
                    .RegisterColumn();
            TestEntityConfigurations.Add(customConfig);
        }

        [TestMethod]
        public async Task CanInsertTestEntityWithCustomNames()
        {
            ConfigureCustomTestEntity();
            BuildTestObjects();

            await TestEntityRepository.Insert(TestEntity);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task CanInsertMultipleOtherEntities(int entities)
        {
            List<OtherEntity> otherEntities = new List<OtherEntity>();
            for (int i = 0; i < entities; i++)
                otherEntities.Add(OtherEntity);
            BuildTestObjects();

            await OtherEntityRepository.Insert(otherEntities);
        }

        [TestMethod]
        public async Task InsertPopulatesNewIdentity()
        {
            BuildTestObjects();

            await OtherEntityRepository.Insert(OtherEntity);

            Assert.AreNotEqual(0, OtherEntity.ID);
        }

        [TestMethod]
        public async Task InsertManyPopulatesNewIdentities()
        {
            List<TestEntity> testEntities = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
                testEntities.Add(new TestEntity() { Name = $"{nameof(InsertManyPopulatesNewIdentities)}{i}" });
            BuildTestObjects();

            await TestEntityRepository.Insert(testEntities);

            Assert.IsTrue(testEntities.All(entity => entity.ID != 0));
            Assert.AreEqual(5, testEntities.GroupBy(entity => entity.ID).Count());
        }

        [TestMethod]
        public async Task InsertManyPopulatesNewIdentitiesWithCustomNames()
        {
            List<TestEntity> testEntities = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
                testEntities.Add(new TestEntity() { Name = $"{nameof(InsertManyPopulatesNewIdentitiesWithCustomNames)}{i}" });
            ConfigureCustomTestEntity();
            BuildTestObjects();

            await TestEntityRepository.Insert(testEntities.OrderByDescending(entity => entity.Name));

            Assert.IsTrue(testEntities.All(entity => entity.ID != 0));
            Assert.AreEqual(5, testEntities.GroupBy(entity => entity.ID).Count());
        }
    }
}
