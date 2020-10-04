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
    public class RepositorySelectIntegrationTest : SqlIntegrationTemplate
    {
        [TestMethod]
        public async Task CanSelectAllEntities()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, OtherEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, OtherEntity>>();

            IList<OtherEntity> entities = await testRepository.FetchAll();

            Assert.AreNotEqual(0, entities.Count);
            Assert.IsFalse(entities.Any(entity => entity.ID == 0));
        }

        [TestMethod]
        public async Task CanSelectAllEntitiesWithCustomNames()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();

            IList<TestEntity> entities = await testRepository.FetchAll();

            Assert.AreNotEqual(0, entities.Count);
            Assert.IsFalse(entities.Any(entity => entity.ID == 0));
        }

        [TestMethod]
        public async Task CanSelectSingleEntity()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
            TestEntity.Name = nameof(CanSelectSingleEntity);
            await testRepository.Insert(TestEntity);

            TestEntity actual = await testRepository.Fetch(TestEntity.ID);

            Assert.AreNotEqual(0, actual.ID);
            Assert.AreNotSame(TestEntity, actual);
            Assert.AreEqual(TestEntity.ID, actual.ID);
            Assert.AreEqual(nameof(CanSelectSingleEntity), actual.Name);
        }

        [TestMethod]
        public async Task CanSelectEnumEntity()
        {
            PopulateEnumEntity();
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEnumEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEnumEntity>>();
            await testRepository.Insert(TestEnumEntity);

            TestEnumEntity actual = await testRepository.Fetch(TestEnumEntity.Keys);

            Assert.AreNotEqual(0, actual.ID);
            Assert.AreNotSame(TestEnumEntity, actual);
            Assert.AreEqual(TestShortEnum.One, actual.ShortEnum);
            Assert.AreEqual(TestLongEnum.One, actual.LongEnum);
            Assert.AreEqual(TestByteEnum.Two, actual.NullableByteEnum);
            Assert.AreEqual(TestIntEnum.Two, actual.NullableIntEnum);
        }
        private void PopulateEnumEntity()
        {
            TestEnumEntity.ShortEnum = TestShortEnum.One;
            TestEnumEntity.LongEnum = TestLongEnum.One;
            TestEnumEntity.NullableByteEnum = TestByteEnum.Two;
            TestEnumEntity.NullableIntEnum = TestIntEnum.Two;
        }

        [TestMethod]
        public async Task CanSelectIdentitylessEntity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, GenericToken> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, GenericToken>>();
            await testRepository.Insert(TestGenericToken);

            GenericToken actual = await testRepository.Fetch(TestGenericToken.ID);

            Assert.AreNotSame(TestGenericToken, actual);
            Assert.AreEqual(TestGenericToken.ID, actual.ID);
            Assert.AreEqual(TestGenericToken.CreateTime, actual.CreateTime);
        }
    }
}
