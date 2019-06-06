using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    using EFCore.Mock;

    [TestClass]
    public class BaseEntity : Template.MockContextTemplate<Mock.BaseEntity>
    {
        protected override Mock.BaseEntity TestObject { get; } = new Mock.BaseEntity();

        [TestMethod]
        public async Task CanFetchAll()
        {
            List<Persistence.Mock.BaseEntity> fetched = await TestObject.FetchAll();
            Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length, fetched.Count);
        }

        [TestMethod]
        public async Task CanFetchAllWithExistingContext()
        {
            using (Context context = TestObject.ExposedContext)
            {
                List<Persistence.Mock.BaseEntity> fetched = await TestObject.FetchAll(context);
                Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length, fetched.Count);
            }
        }

        [TestMethod]
        public async Task CanCount()
        {
            Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length, await TestObject.Count());
        }

        [TestMethod]
        public async Task CanCountWithExistingContext()
        {
            using (Context context = TestObject.ExposedContext)
            {
                Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length, await TestObject.Count(context));
            }
        }

        [TestMethod]
        public async Task CanInsert()
        {
            await TestObject.Insert(InsertEntity);
            Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length + 1, await TestObject.Count());
        }
        private Persistence.Mock.BaseEntity InsertEntity => new Persistence.Mock.BaseEntity() { MyValue = InsertValue };
        private const int InsertValue = 15;

        [TestMethod]
        public async Task CanInsertWithContext()
        {
            using (Context context = TestObject.ExposedContext)
            {
                await TestObject.Insert(context, InsertEntity);
                await context.SaveChangesAsync();
                Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length + 1, await TestObject.Count());
            }
        }

        [TestMethod]
        public async Task InsertFiresEvent()
        {
            await TestObject.Insert(InsertEntity);
            Assert.AreEqual(1, TestObject.EntitiesAdded.Count);
            Assert.AreEqual(InsertValue, TestObject.EntitiesAdded[0].Entity.MyValue);
        }

        [TestMethod]
        public async Task InsertWithContextFiresEvent()
        {
            using (Context context = TestObject.ExposedContext)
            {
                await TestObject.Insert(context, InsertEntity);
                await context.SaveChangesAsync();
                Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length + 1, await TestObject.Count());
            }
        }
    }
}
