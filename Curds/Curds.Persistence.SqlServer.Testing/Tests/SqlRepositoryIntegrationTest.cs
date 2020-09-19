using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SqlRepositoryIntegrationTest : SqlIntegrationTemplate
    {
        private TestEntity TestEntity = new TestEntity();
        private OtherEntity OtherEntity = new OtherEntity();
        private TestEnumEntity TestEnumEntity = new TestEnumEntity();

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
        public async Task CanInsertTestEnumEntity()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEnumEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEnumEntity>>();

            await testRepository.Insert(TestEnumEntity);
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
        public async Task CanDeleteSingleEntity()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
            TestEntity.Name = nameof(CanDeleteSingleEntity);
            await testRepository.Insert(TestEntity);

            await testRepository.Delete(TestEntity.ID);

            try
            {
                await testRepository.Fetch(TestEntity.ID);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            { }
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
        public async Task CanUpdateWithConstantValues()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
            await testRepository.Insert(TestEntity);

            await testRepository
                .Update(TestEntity.ID)
                .Set(entity => entity.Name, nameof(CanUpdateWithConstantValues))
                .Execute();

            TestEntity actual = await testRepository.Fetch(TestEntity.ID);
            Assert.AreEqual(TestEntity.ID, actual.ID);
            Assert.AreEqual(nameof(CanUpdateWithConstantValues), actual.Name);
            Assert.AreNotSame(TestEntity, actual);
        }

        [TestMethod]
        public async Task CanProjectEntityFromJoin()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, Parent> parentRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, Parent>>();
            IChildRepository childRepository = TestServiceProvider.GetRequiredService<IChildRepository>();
            Parent testParent = new Parent { Name = nameof(testParent) };
            await parentRepository.Insert(testParent);
            List<Child> testChildren = new List<Child>();
            for (int i = 0; i < 3; i++)
            {
                Child testChild = new Child
                {
                    ParentID = testParent.ID,
                    Name = $"{nameof(testChild)}{i}",
                };
                testChildren.Add(testChild);
            }
            await childRepository.Insert(testChildren);

            List<Child> actual = await childRepository.ChildrenByParent(testParent.ID);

            CollectionAssert.AreEqual(testChildren, actual);
        }

        [TestMethod]
        public async Task CanJoinOnMultipleCriteria()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, Parent> parentRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, Parent>>();
            IChildRepository childRepository = TestServiceProvider.GetRequiredService<IChildRepository>();
            Parent testParent = new Parent { Name = nameof(testParent) };
            await parentRepository.Insert(testParent);
            List<Child> testChildren = new List<Child>();
            for (int i = 0; i < 3; i++)
            {
                Child testChild = new Child
                {
                    ParentID = testParent.ID,
                    Name = $"{nameof(testChild)}{i}",
                };
                testChildren.Add(testChild);
            }
            await childRepository.Insert(testChildren);

            List<Child> actual = await childRepository.OddChildrenByParent(testParent.ID);

            List<Child> oddChildren = testChildren
                .Where(child => child.ID % 2 != 0)
                .ToList();
            CollectionAssert.AreEqual(oddChildren, actual);
        }

        [TestMethod]
        public async Task CanFilterJoinedUniverse()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, Parent> parentRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, Parent>>();
            IChildRepository childRepository = TestServiceProvider.GetRequiredService<IChildRepository>();
            Parent testParent = new Parent { Name = nameof(testParent) };
            await parentRepository.Insert(testParent);
            List<Child> testChildren = new List<Child>();
            for (int i = 0; i < 3; i++)
            {
                Child testChild = new Child
                {
                    ParentID = testParent.ID,
                    Name = $"{nameof(testChild)}{i}",
                };
                testChildren.Add(testChild);
            }
            await childRepository.Insert(testChildren);

            List<Child> actual = await childRepository.EvenChildrenByParent(testParent.ID);

            List<Child> evenChildren = testChildren
                .Where(child => child.ID % 2 == 0)
                .ToList();
            CollectionAssert.AreEqual(evenChildren, actual);
        }
    }
}
