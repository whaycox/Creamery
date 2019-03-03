using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using Curds.Domain.Persistence;
using System.Linq;
using Curds;

namespace Curds.Application.Persistence
{
    public abstract class IPersistorTemplate<T, U> : TestTemplate<T> where T : IPersistor<U> where U : Entity
    {
        protected abstract U Sample { get; }
        protected abstract Func<U, U> Modifier { get; }

        protected abstract U SampleInLoop(int iteration);
        protected abstract void VerifySampleInLoop(U sample, int iteration);

        [TestMethod]
        public void FetchAllWorksWhenEmpty()
        {
            foreach (U test in TestObject.FetchAll().AwaitResult())
                Assert.Fail();
        }

        [TestMethod]
        public void LookupManyWorksWhenEmpty()
        {
            foreach (U found in TestObject.Lookup(new int[] { -1, 0, 1 }))
                Assert.Fail();
        }

        [TestMethod]
        public void CanInsertNewItem()
        {
            Assert.AreEqual(0, TestObject.Count);
            TestObject.Insert(Sample);
            Assert.AreEqual(1, TestObject.Count);
        }

        [TestMethod]
        public void ReturnOfInsertHasNewID()
        {
            U sample = Sample;
            Assert.AreEqual(default(int), sample.ID);
            var returned = TestObject.Insert(sample).AwaitResult();
            Assert.AreEqual(1, sample.ID);
            Assert.AreEqual(1, returned.ID);
        }

        [TestMethod]
        public void InsertWithExistingIDThrows()
        {
            U sample = Sample;
            sample.ID = 5;
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Insert(sample));
        }

        [TestMethod]
        public void LookupInvalidKeyThrowsError()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(-1));
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(0));
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(1));
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
            TestObject.Insert(sample);
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
            TestObject.Insert(sample);
            int id = sample.ID;
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Update(id, UpdateIDThrowsExceptionHelper));
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
            TestObject.Insert(sample);
            TestObject.Delete(sample.ID);
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Lookup(sample.ID));
        }

        [TestMethod]
        public void LookupManyOnlyReturnsExpected()
        {
            for (int i = 1; i <= 10; i++)
                TestObject.Insert(SampleInLoop(i));
            var retrieved = TestObject.Lookup(new int[] { 2, 5, 8 }).ToArray();
            Assert.AreEqual(3, retrieved.Length);
            VerifySampleInLoop(retrieved[0], 2);
            VerifySampleInLoop(retrieved[1], 5);
            VerifySampleInLoop(retrieved[2], 8);
        }

        [TestMethod]
        public void ModifyLookupManyDoesntModifyPersistedCopy()
        {
            for (int i = 1; i <= 10; i++)
                TestObject.Insert(SampleInLoop(i));
            var retrieved = TestObject.Lookup(new int[] { 2, 5, 8 }).ToArray();
            retrieved[0] = Modifier(retrieved[0]);
            U singleRetrieve = TestObject.Lookup(2).AwaitResult();
            Assert.AreNotSame(retrieved[0], singleRetrieve);
            Assert.AreNotEqual(retrieved[0], singleRetrieve);
        }

        [TestMethod]
        public void LookupManyCanAcceptInvalidValues()
        {
            for (int i = 1; i <= 10; i++)
                TestObject.Insert(SampleInLoop(i));
            var retrieved = TestObject.Lookup(new int[] { -1, 0, 2, 5, 8 }).ToArray();
            Assert.AreEqual(3, retrieved.Length);
            VerifySampleInLoop(retrieved[0], 2);
            VerifySampleInLoop(retrieved[1], 5);
            VerifySampleInLoop(retrieved[2], 8);
        }

    }
}
