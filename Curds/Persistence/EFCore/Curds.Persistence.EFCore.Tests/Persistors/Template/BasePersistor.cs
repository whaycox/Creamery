using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistors.Template
{
    using EFCore.Template;

    public abstract class BasePersistor<T, U, V> : DbContext<T, U>
        where T : Implementation.BasePersistor<U, V>
        where U : DbContext
        where V : class
    {
        protected abstract int ExpectedStartingCount { get; }
        protected abstract U ExposedContext { get; }
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
            using (U context = ExposedContext)
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
            using (U context = ExposedContext)
                Assert.AreEqual(ExpectedStartingCount, await TestObject.Count(context));
        }

        [TestMethod]
        public async Task CanInsert()
        {
            await TestObject.Insert(InsertEntity);
            Assert.AreEqual(ExpectedStartingCount + 1, await TestObject.Count());
        }

        [TestMethod]
        public async Task CanInsertWithExistingContext()
        {
            using (U context = ExposedContext)
            {
                await TestObject.Insert(context, InsertEntity);
                await context.SaveChangesAsync();
                Assert.AreEqual(ExpectedStartingCount + 1, await TestObject.Count());
            }
        }
    }
}
