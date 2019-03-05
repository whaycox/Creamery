using Curds.Domain;
using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace Curds.Application.Persistence
{
    public abstract class IPersistorTemplate<T, U> : CronTemplate<T> where T : IPersistor<U> where U : Entity
    {
        private const int ConcurrentIterations = 10;

        protected abstract U Sample { get; }
        protected abstract Func<U, U> Modifier { get; }

        [TestMethod]
        public void FetchAllWorksWhenEmpty()
        {
            foreach (U test in TestObject.FetchAll().AwaitResult())
                Assert.Fail();
        }

        [TestMethod]
        public void CanInsertNewItem()
        {
            Assert.AreEqual(0, TestObject.Count.AwaitResult());
            TestObject.Insert(Sample).AwaitResult();
            Assert.AreEqual(1, TestObject.Count.AwaitResult());
        }

        [TestMethod]
        public void CanConcurrentlyInsertNewItems()
        {
            U[] results = new U[ConcurrentIterations];
            Task[] tasks = new Task[ConcurrentIterations];
            for (int i = 0; i < ConcurrentIterations; i++)
            {
                int index = i;
                tasks[i] = Task.Factory.StartNew(() => ConcurrentInsertHelper(index, results));
            }
            Task.WaitAll(tasks);
            Assert.IsFalse(results.Where(r => r == null).Any());
            Assert.IsFalse(results.Where(r => r.ID == default(int)).Any());
            Assert.IsFalse(results.GroupBy(g => g.ID).Where(g => g.Count() != 1).Any());
        }
        private void ConcurrentInsertHelper(int index, U[] results)
        {
            Debug.WriteLine($"Inserting with index {index}");
            results[index] = TestObject.Insert(Sample).AwaitResult();
        }

        [TestMethod]
        public void ReturnOfInsertHasNewID()
        {
            U sample = Sample;
            Assert.AreEqual(default(int), sample.ID);
            U returned = TestObject.Insert(sample).AwaitResult();
            Assert.AreNotEqual(default(int), sample.ID);
            Assert.AreNotEqual(default(int), returned.ID);
        }

        [TestMethod]
        public void InsertWithExistingIDThrows()
        {
            U sample = Sample;
            sample.ID = 5;
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Insert(sample).AwaitResult());
        }

        [TestMethod]
        public void LookupInvalidKeyThrowsError()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(-1).AwaitResult());
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(0).AwaitResult());
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(1).AwaitResult());
        }

        [TestMethod]
        public void LookupReturnsCopyOfEntity()
        {
            U sample = Sample;
            TestObject.Insert(sample);
            U retrieved = TestObject.Lookup(sample.ID).AwaitResult();
            Assert.AreNotSame(sample, retrieved);
            Assert.AreEqual(sample, retrieved);
        }

        [TestMethod]
        public void ModifyRetrievedEntityDoesntModifyPersistedCopy()
        {
            U sample = Sample;
            TestObject.Insert(sample);
            U first = TestObject.Lookup(sample.ID).AwaitResult();
            first = Modifier(first);
            U second = TestObject.Lookup(sample.ID).AwaitResult();
            Assert.AreNotSame(first, second);
            Assert.AreNotEqual(first, second);
        }

        [TestMethod]
        public void CanUpdateEntity()
        {
            U sample = Sample;
            TestObject.Insert(sample).AwaitResult();
            TestObject.Update(sample.ID, Modifier).AwaitResult();
            U retrieved = TestObject.Lookup(sample.ID).AwaitResult();
            Assert.AreNotEqual(sample, retrieved);
            sample = Modifier(sample);
            Assert.AreEqual(sample, retrieved);
        }

        [TestMethod]
        public void UpdateIDThrowsException()
        {
            U sample = Sample;
            TestObject.Insert(sample).AwaitResult();
            int id = sample.ID;
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Update(id, UpdateIDThrowsExceptionHelper).AwaitResult());
        }
        private U UpdateIDThrowsExceptionHelper(U entity)
        {
            entity.ID++;
            return entity;
        }

        [TestMethod]
        public void CanDeleteEntity()
        {
            U sample = Sample;
            TestObject.Insert(sample).AwaitResult();
            TestObject.Delete(sample.ID).AwaitResult();
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(sample.ID).AwaitResult());
        }
    }
}
