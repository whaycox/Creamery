using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Template
{
    public abstract class Entity<T, U, V> : BaseEntity<T, U, V>
        where T : Implementation.Entity<U, V>
        where U : DbContext
        where V : Persistence.Domain.Entity
    {
        protected abstract int SeededID { get; }
        protected abstract int NonSeededID { get; }

        protected V ExistingIDEntity
        {
            get
            {
                V toReturn = InsertEntity;
                toReturn.ID = 1;
                return toReturn;
            }
        }

        [TestMethod]
        public async Task CannotInsertWithExistingID()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => TestObject.Insert(ExistingIDEntity));
        }

        [TestMethod]
        public async Task CannotInsertWithExistingIDUsingContext()
        {
            using (U context = RetrieveExposedContext)
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => TestObject.Insert(context, ExistingIDEntity));
        }

        [TestMethod]
        public async Task CanDelete()
        {
            await TestObject.Delete(SeededID);
            Assert.AreEqual(ExpectedStartingCount - 1, await TestObject.Count());
        }

        [TestMethod]
        public async Task DeleteThrowsWithInvalidID()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Delete(NonSeededID));
        }

        [TestMethod]
        public async Task CanDeleteWithExistingContext()
        {
            using (U context = RetrieveExposedContext)
            {
                await TestObject.Delete(context, SeededID);
                await context.SaveChangesAsync();
                Assert.AreEqual(ExpectedStartingCount - 1, await TestObject.Count());
            }
        }

        [TestMethod]
        public async Task DeleteWithContextThrowsWithInvalidID()
        {
            using (U context = RetrieveExposedContext)
                await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Delete(context, NonSeededID));
        }

        [TestMethod]
        public async Task DeleteFiresEvent()
        {
            await TestObject.Delete(SeededID);
            VerifyDeleteEventFired();
        }
        protected abstract void VerifyDeleteEventFired();

        [TestMethod]
        public async Task DeleteWithContextFiresEvent()
        {
            using (U context = RetrieveExposedContext)
            {
                await TestObject.Delete(SeededID);
                await context.SaveChangesAsync();
                VerifyDeleteEventFired();
            }
        }

        [TestMethod]
        public async Task CanLookup()
        {
            var fetched = await TestObject.Lookup(SeededID);
            Assert.AreEqual(SeededID, fetched.ID);
        }

        [TestMethod]
        public async Task CanLookupWithExistingContext()
        {
            using (U context = RetrieveExposedContext)
            {
                var fetched = await TestObject.Lookup(context, SeededID);
                Assert.AreEqual(SeededID, fetched.ID);
            }

        }

        [TestMethod]
        public async Task CanLookupMany()
        {
            int[] ids = ManySeededIDs();
            List<V> fetched = await TestObject.LookupMany(ids);
            Assert.AreEqual(ExpectedStartingCount, fetched.Count);
            Assert.IsFalse(fetched.Where(i => !ids.Contains(i.ID)).Any());
        }
        protected abstract int[] ManySeededIDs();

        [TestMethod]
        public async Task InvalidIDInLookupManyThrows()
        {
            int[] ids = ManySeededIDs();
            ids[0] = NonSeededID;
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.LookupMany(ids));
        }

        [TestMethod]
        public async Task CanUpdate()
        {
            await TestObject.Update(SeededID, Modifier);
            var fetched = await TestObject.Lookup(SeededID);
            VerifyModifiedEntity(fetched);
        }
        protected abstract V Modifier(V entity);
        protected abstract void VerifyModifiedEntity(V fetched);

        [TestMethod]
        public async Task CanUpdateWithExistingContext()
        {
            using (U context = RetrieveExposedContext)
            {
                await TestObject.Update(context, SeededID, Modifier);
                await context.SaveChangesAsync();
                var fetched = await TestObject.Lookup(SeededID);
                VerifyModifiedEntity(fetched);
            }
        }

        [TestMethod]
        public async Task UpdateThrowsWithInvalidID()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Update(NonSeededID, Modifier));
        }

        [TestMethod]
        public async Task UpdateFiresEvent()
        {
            await TestObject.Update(SeededID, Modifier);
            VerifyUpdateEventFired();
        }
        protected abstract void VerifyUpdateEventFired();

        [TestMethod]
        public async Task UpdateWithContextFiresEvent()
        {
            using (U context = RetrieveExposedContext)
            {
                await TestObject.Update(context, SeededID, Modifier);
                await context.SaveChangesAsync();
                VerifyUpdateEventFired();
            }
        }

        [TestMethod]
        public async Task UpdateWithContextThrowsWithInvalidID()
        {
            using (U context = RetrieveExposedContext)
                await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Update(context, NonSeededID, Modifier));
        }
    }
}
