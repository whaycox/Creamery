using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence.Tests
{
    using Curds.Template;
    using Abstraction;
    using Curds.Domain;

    [TestClass]
    public class CloningSimpleEntityRepositoryIntegrationTest : IntegrationTemplate
    {
        private TestEntity TestEntity = new TestEntity();

        [TestMethod]
        public async Task CanStoreAndRetrieve()
        {
            RegisterServices();
            BuildServiceProvider();
            ISimpleRepository<TestEntity> testRepository = TestServiceProvider.GetRequiredService<ISimpleRepository<TestEntity>>();

            await testRepository.Insert(TestEntity);
            TestEntity fetched = await testRepository.Fetch(TestEntity.ID);

            Assert.AreNotEqual(0, fetched.ID);
            Assert.AreEqual(TestEntity.ID, fetched.ID);
            Assert.AreNotSame(TestEntity, fetched);
        }

        [TestMethod]
        public async Task CanStoreAndDelete()
        {
            RegisterServices();
            BuildServiceProvider();
            ISimpleRepository<TestEntity> testRepository = TestServiceProvider.GetRequiredService<ISimpleRepository<TestEntity>>();

            await testRepository.Insert(TestEntity);
            await testRepository.Delete(TestEntity.ID);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => testRepository.Fetch(TestEntity.ID));
        }

        [TestMethod]
        public async Task CanStoreAndUpdate()
        {
            RegisterServices();
            BuildServiceProvider();
            ISimpleRepository<TestEntity> testRepository = TestServiceProvider.GetRequiredService<ISimpleRepository<TestEntity>>();

            await testRepository.Insert(TestEntity);
            await testRepository.Update(TestEntity.ID)
                .Set(testEntity => testEntity.NullableIntValue, TestEntity.IntValue)
                .Set(testEntity => testEntity.DateTimeValue, DateTime.MaxValue)
                .Execute();

            TestEntity actual = await testRepository.Fetch(TestEntity.ID);
            Assert.IsNull(TestEntity.NullableIntValue);
            Assert.AreEqual(TestEntity.IntValue, actual.NullableIntValue);
            Assert.AreNotEqual(TestEntity.DateTimeValue, actual.DateTimeValue);
            Assert.AreEqual(DateTime.MaxValue, actual.DateTimeValue);
            Assert.AreNotSame(TestEntity, actual);
        }
    }
}
