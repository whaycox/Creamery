using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Template;

    [TestClass]
    public class RepositoryInsertIntegrationTest : SqlIntegrationTemplate
    {
        [TestMethod]
        public async Task CanInsertTestEntity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();

            await testRepository.Insert(TestEntity);
        }

        [TestMethod]
        public async Task CanInsertOtherEntity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, OtherEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();

            await testRepository.Insert(OtherEntity);
        }

        [TestMethod]
        public async Task CanInsertTestEnumEntity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEnumEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEnumEntity>>();

            await testRepository.Insert(TestEnumEntity);
        }

        [TestMethod]
        public async Task CanInsertIdentitylessEntity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, GenericToken> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, GenericToken>>();

            await testRepository.Insert(TestGenericToken);
        }

        [TestMethod]
        public async Task CanInsertPopulatedOtherEntity()
        {
            FullyPopulateOtherEntity();
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, OtherEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();

            await testRepository.Insert(OtherEntity);
        }

        [TestMethod]
        public async Task CanInsertTestEntityWithCustomNames()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();

            await testRepository.Insert(TestEntity);
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
            IRepository<ITestDataModel, OtherEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();

            await testRepository.Insert(otherEntities);
        }

        [TestMethod]
        public async Task InsertPopulatesNewIdentity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, OtherEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();

            await testRepository.Insert(OtherEntity);

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
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();

            await testRepository.Insert(testEntities);

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
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();

            await testRepository.Insert(testEntities.OrderByDescending(entity => entity.Name));

            Assert.IsTrue(testEntities.All(entity => entity.ID != 0));
            Assert.AreEqual(5, testEntities.GroupBy(entity => entity.ID).Count());
        }
    }
}
