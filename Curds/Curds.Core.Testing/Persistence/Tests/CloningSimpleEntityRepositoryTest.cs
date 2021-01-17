using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Clone.Abstraction;
    using Domain;
    using Expressions.Abstraction;
    using Implementation;

    [TestClass]
    public class CloningSimpleEntityRepositoryTest
    {
        private static IEnumerable<object[]> InvalidKeys => new List<object[]>
        {
            { new object[] { null } },
            { new object[] { new object[0] } },
            { new object[] { new object[] { nameof(InvalidKeys) } } },
            { new object[] { new object[] { true } } },
            { new object[] { new object[] { 1, 2 } } },
        };

        private TestSimpleEntity TestEntity = new TestSimpleEntity();
        private TestSimpleEntity TestClonedEntity = new TestSimpleEntity();

        private Mock<ICloneFactory> MockCloneFactory = new Mock<ICloneFactory>();
        private Mock<IExpressionParser> MockExpressionParser = new Mock<IExpressionParser>();
        private Mock<IEntityUpdateDelegateFactory> MockUpdateDelegateFactory = new Mock<IEntityUpdateDelegateFactory>();

        private CloningSimpleEntityRepository<TestSimpleEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockCloneFactory
                .Setup(factory => factory.Clone(It.IsAny<TestSimpleEntity>()))
                .Returns(TestClonedEntity);

            TestObject = new CloningSimpleEntityRepository<TestSimpleEntity>(
                MockCloneFactory.Object,
                MockExpressionParser.Object,
                MockUpdateDelegateFactory.Object);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(100)]
        public async Task InsertingEntitiesClonesEach(int testEntityCount)
        {
            List<TestSimpleEntity> testEntities = new List<TestSimpleEntity>();
            for (int i = 0; i < testEntityCount; i++)
                testEntities.Add(new TestSimpleEntity());

            await TestObject.Insert(testEntities);

            for (int i = 0; i < testEntities.Count; i++)
                MockCloneFactory.Verify(factory => factory.Clone(testEntities[i]), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(100)]
        public async Task InsertingEntitiesAssignsNextIdentity(int testEntityCount)
        {
            List<TestSimpleEntity> testEntities = new List<TestSimpleEntity>();
            for (int i = 0; i < testEntityCount; i++)
                testEntities.Add(new TestSimpleEntity());

            await TestObject.Insert(testEntities);

            for (int i = 0; i < testEntities.Count; i++)
                Assert.AreEqual(CloningSimpleEntityRepository<TestSimpleEntity>.StartingIdentity + i, testEntities[i].ID);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task InsertingPopulatedIdentityThrows()
        {
            TestEntity.ID = 10;

            await TestObject.Insert(TestEntity);
        }

        [TestMethod]
        public async Task FetchingEntityClonesIt()
        {
            await TestObject.Insert(TestEntity);

            await TestObject.Fetch(TestEntity.ID);

            MockCloneFactory.Verify(factory => factory.Clone(TestClonedEntity), Times.Once);
        }

        [TestMethod]
        public async Task CanFetchWithParamsKeys()
        {
            await TestObject.Insert(TestEntity);

            await TestObject.Fetch(keys: new object[] { TestEntity.ID });
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidKeys))]
        [ExpectedException(typeof(ArgumentException))]
        public async Task FetchingWithNonIntKeysThrows(object[] testKeys)
        {
            await TestObject.Fetch(testKeys);
        }

        [TestMethod]
        public async Task FetchReturnsClonedEntity()
        {
            await TestObject.Insert(TestEntity);

            TestSimpleEntity actual = await TestObject.Fetch(TestEntity.ID);

            Assert.AreSame(TestClonedEntity, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task FetchingByInvalidIDThrows()
        {
            await TestObject.Fetch(1);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(100)]
        public async Task FetchAllReturnsStoredEntities(int testEntityCount)
        {
            List<TestSimpleEntity> testEntities = new List<TestSimpleEntity>();
            for (int i = 0; i < testEntityCount; i++)
            {
                TestSimpleEntity testEntity = new TestSimpleEntity();
                MockCloneFactory
                    .Setup(factory => factory.Clone(testEntity))
                    .Returns(testEntity);
                testEntities.Add(testEntity);
            }
            await TestObject.Insert(testEntities);

            List<TestSimpleEntity> actual = await TestObject.FetchAll();

            CollectionAssert.AreEqual(testEntities, actual);
        }

        [TestMethod]
        public async Task DeleteRemovesEntityFromCollection()
        {
            await TestObject.Insert(TestEntity);

            await TestObject.Delete(TestEntity.ID);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Fetch(TestEntity.ID));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task DeletingWithInvalidIDThrows()
        {
            await TestObject.Delete(1);
        }

        [TestMethod]
        public async Task CanDeleteWithParamsKeys()
        {
            await TestObject.Insert(TestEntity);

            await TestObject.Delete(keys: new object[] { TestEntity.ID });
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidKeys))]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DeletingWithNonIntKeysThrows(object[] testKeys)
        {
            await TestObject.Delete(testKeys);
        }

        [TestMethod]
        public async Task UpdateReturnsExpectedType()
        {
            await TestObject.Insert(TestEntity);

            IEntityUpdate<TestSimpleEntity> actual = TestObject.Update(TestEntity.ID);

            Assert.IsInstanceOfType(actual, typeof(SimpleEntityUpdate<TestSimpleEntity>));
        }

        [TestMethod]
        public async Task UpdatePopulatesEntity()
        {
            await TestObject.Insert(TestEntity);

            IEntityUpdate<TestSimpleEntity> actual = TestObject.Update(TestEntity.ID);

            SimpleEntityUpdate<TestSimpleEntity> actualUpdate = (SimpleEntityUpdate<TestSimpleEntity>)actual;
            Assert.AreSame(TestClonedEntity, actualUpdate.UpdatingEntity);
        }

        [TestMethod]
        public async Task UpdatePopulatesExpressionParser()
        {
            await TestObject.Insert(TestEntity);

            IEntityUpdate<TestSimpleEntity> actual = TestObject.Update(TestEntity.ID);

            SimpleEntityUpdate<TestSimpleEntity> actualUpdate = (SimpleEntityUpdate<TestSimpleEntity>)actual;
            Assert.AreSame(MockExpressionParser.Object, actualUpdate.ExpressionParser);
        }

        [TestMethod]
        public async Task UpdatePopulatesUpdateDelegateFactory()
        {
            await TestObject.Insert(TestEntity);

            IEntityUpdate<TestSimpleEntity> actual = TestObject.Update(TestEntity.ID);

            SimpleEntityUpdate<TestSimpleEntity> actualUpdate = (SimpleEntityUpdate<TestSimpleEntity>)actual;
            Assert.AreSame(MockUpdateDelegateFactory.Object, actualUpdate.UpdateDelegateFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void UpdatingWithInvalidIDThrows()
        {
            TestObject.Update(1);
        }

        [TestMethod]
        public async Task CanUpdateWithParamsKeys()
        {
            await TestObject.Insert(TestEntity);

            TestObject.Update(keys: new object[] { TestEntity.ID });
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidKeys))]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatingWithNonIntKeysThrows(object[] testKeys)
        {
            TestObject.Update(testKeys);
        }
    }
}
