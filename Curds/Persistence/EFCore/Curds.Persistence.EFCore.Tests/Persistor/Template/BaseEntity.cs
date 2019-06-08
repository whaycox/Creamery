using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Template
{
    using EFCore.Template;

    public abstract class BaseEntity<T, U, V> : MockContextTemplate<T>
        where T : Implementation.BaseEntity<U, V>
        where U : DbContext
        where V : Persistence.Domain.BaseEntity
    {
        protected abstract int ExpectedStartingCount { get; }
        protected abstract U RetrieveExposedContext { get; }
        protected abstract V InsertEntity { get; }

        [TestMethod]
        public async Task CanFetchAll()
        {
            List<V> fetched = await TestObject.FetchAll();
            Assert.AreEqual(ExpectedStartingCount, fetched.Count);
        }

        [TestMethod]
        public async Task CanFetchAllWithExistingContext()
        {
            using (U context = RetrieveExposedContext)
            {
                List<V> fetched = await TestObject.FetchAll(context);
                Assert.AreEqual(ExpectedStartingCount, fetched.Count);
            }
        }

        [TestMethod]
        public async Task CanCount()
        {
            Assert.AreEqual(ExpectedStartingCount, await TestObject.Count());
        }

        [TestMethod]
        public async Task CanCountWithExistingContext()
        {
            using (U context = RetrieveExposedContext)
            {
                Assert.AreEqual(ExpectedStartingCount, await TestObject.Count(context));
            }
        }

        [TestMethod]
        public async Task CanInsert()
        {
            await TestObject.Insert(InsertEntity);
            Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length + 1, await TestObject.Count());
        }

        [TestMethod]
        public async Task CanInsertWithContext()
        {
            using (U context = RetrieveExposedContext)
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
            VerifyInsertEventFired();
        }
        protected abstract void VerifyInsertEventFired();

        [TestMethod]
        public async Task InsertWithContextFiresEvent()
        {
            using (U context = RetrieveExposedContext)
            {
                await TestObject.Insert(context, InsertEntity);
                await context.SaveChangesAsync();
                VerifyInsertEventFired();
            }
        }
    }
}
