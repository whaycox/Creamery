using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistors.Template
{
    using Persistence.Abstraction;

    public abstract class EntityPersistor<T, U, V> : BasePersistor<T, U, V>
        where T : Implementation.EntityPersistor<U, V>
        where U : DbContext
        where V : class, IEntity
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

        protected abstract V Modifier(V entity);
        protected abstract void VerifyModifiedEntity(V fetched);

        [TestMethod]
        public async Task CannotInsertWithExistingID()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => TestObject.Insert(ExistingIDEntity));
        }

        [TestMethod]
        public async Task CannotInsertWithExistingIDUsingContext()
        {
            using (U context = BuildContext())
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
            using (U context = ExposedContext)
            {
                await TestObject.Delete(context, SeededID);
                await context.SaveChangesAsync();
                Assert.AreEqual(ExpectedStartingCount - 1, await TestObject.Count());
            }
        }

        [TestMethod]
        public async Task DeleteWithContextThrowsWithInvalidID()
        {
            using (U context = BuildContext())
                await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Delete(context, NonSeededID));
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
            using (U context = ExposedContext)
            {
                var fetched = await TestObject.Lookup(context, SeededID);
                Assert.AreEqual(SeededID, fetched.ID);
            }
        }

        [TestMethod]
        public async Task CanLookupMany()
        {
            int[] ids = ExpectedStartingIDs();
            List<V> fetched = await TestObject.LookupMany(ids);
            Assert.AreEqual(ExpectedStartingCount, fetched.Count);
            Assert.IsFalse(fetched.Where(i => !ids.Contains(i.ID)).Any());
        }
        protected abstract int[] ExpectedStartingIDs();

        [TestMethod]
        public async Task InvalidIDInLookupManyThrows()
        {
            int[] ids = ExpectedStartingIDs();
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

        [TestMethod]
        public async Task CanUpdateWithExistingContext()
        {
            using (U context = ExposedContext)
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
        public async Task UpdateWithContextThrowsWithInvalidID()
        {
            using (U context = BuildContext())
                await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => TestObject.Update(context, NonSeededID, Modifier));
        }
    }
}
