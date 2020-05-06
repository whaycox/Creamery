using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whey.Domain;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Template;

    [TestClass]
    [TestCategory(nameof(TestType.Integration))]
    public class SqlRepositoryIntegrationTest : SqlTemplate
    {
        private TestEntity TestEntity = new TestEntity();
        private OtherEntity OtherEntity = new OtherEntity();
        private string TestSchema = nameof(TestSchema);

        private IServiceCollection TestServiceCollection = new ServiceCollection();
        private IServiceProvider TestServiceProvider = null;

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
            TestServiceCollection.Configure<SqlConnectionInformation>(info =>
            {
                info.Server = TestServer;
                info.Database = TestDatabase;
            });
        }

        private void RegisterServices()
        {
            TestServiceCollection
                .AddCurdsPersistence();
        }

        private void BuildServiceProvider()
        {
            TestServiceProvider = TestServiceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task CanInsertTestEntity()
        {
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, TestEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
                await testRepository.Insert(TestEntity);
            }
        }

        [TestMethod]
        public async Task CanInsertOtherEntity()
        {
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, OtherEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();
                await testRepository.Insert(OtherEntity);
            }
        }

        [TestMethod]
        public async Task CanInsertPopulatedOtherEntity()
        {
            FullyPopulateOtherEntity();
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, OtherEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();
                await testRepository.Insert(OtherEntity);
            }
        }

        private void ConfigureCustomTestEntity()
        {
            TestServiceCollection
                .ConfigureDefaultSchema(TestSchema)
                .ConfigureEntity<TestEntity>()
                    .WithTableName("TestCustomEntity")
                    .ConfigureColumn(entity => entity.ID)
                        .WithColumnName("CustomIdentityField")
                        .RegisterColumn()
                    .ConfigureColumn(entity => entity.Name)
                        .WithColumnName("SomeOtherName")
                        .RegisterColumn()
                    .RegisterEntity();
        }

        [TestMethod]
        public async Task CanInsertTestEntityWithCustomNames()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, TestEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
                await testRepository.Insert(TestEntity);
            }
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
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, OtherEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();
                await testRepository.Insert(OtherEntity);
            }
        }

        [TestMethod]
        public async Task InsertPopulatesNewIdentity()
        {
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, OtherEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();
                await testRepository.Insert(OtherEntity);
            }

            Assert.AreNotEqual(0, OtherEntity.ID);
        }

        [TestMethod]
        public async Task InsertManyPopulatesNewIdentities()
        {
            List<TestEntity> testEntities = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
                testEntities.Add(new TestEntity { Name = $"{nameof(InsertManyPopulatesNewIdentities)}{i}" });
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, TestEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
                await testRepository.Insert(testEntities);
            }

            Assert.IsTrue(testEntities.All(entity => entity.ID != 0));
            Assert.AreEqual(5, testEntities.GroupBy(entity => entity.ID).Count());
        }

        [TestMethod]
        public async Task InsertManyPopulatesNewIdentitiesWithCustomNames()
        {
            List<TestEntity> testEntities = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
                testEntities.Add(new TestEntity { Name = $"{nameof(InsertManyPopulatesNewIdentitiesWithCustomNames)}{i}" });
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, TestEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
                await testRepository.Insert(testEntities.OrderByDescending(entity => entity.Name));
            }

            Assert.IsTrue(testEntities.All(entity => entity.ID != 0));
            Assert.AreEqual(5, testEntities.GroupBy(entity => entity.ID).Count());
        }

        [TestMethod]
        public async Task CanSelectAllEntities()
        {
            RegisterServices();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, OtherEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();
                IList<OtherEntity> entities = await testRepository.FetchAll();

                Assert.AreNotEqual(0, entities.Count);
                Assert.IsFalse(entities.Any(entity => entity.ID == 0));
            }
        }

        [TestMethod]
        public async Task CanSelectAllEntitiesWithCustomNames()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, TestEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
                IList<TestEntity> entities = await testRepository.FetchAll();

                Assert.AreNotEqual(0, entities.Count);
                Assert.IsFalse(entities.Any(entity => entity.ID == 0));
            }
        }

        [TestMethod]
        public async Task CanSelectSingleEntity()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                IRepository<ITestDataModel, TestEntity> testRepository = testScope.ServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
                TestEntity.Name = nameof(CanSelectSingleEntity);
                await testRepository.Insert(TestEntity);

                TestEntity actual = await testRepository.Fetch(TestEntity.ID);

                Assert.AreNotEqual(0, actual.ID);
                Assert.AreNotSame(TestEntity, actual);
                Assert.AreEqual(TestEntity.ID, actual.ID);
                Assert.AreEqual(nameof(CanSelectSingleEntity), actual.Name);
            }

        }
    }
}
