﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Persistor.Template
{
    using Abstraction;
    using Domain;

    public abstract class IEntityPersistorTemplate<T, U> : IPersistorTemplate<T, U> where T : IEntityPersistor<U> where U : Entity
    {
        private const int ConcurrentIterations = 10;

        protected abstract Func<U, U> Modifier { get; }

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
            TestObject.Insert(sample).AwaitResult();
            U retrieved = TestObject.Lookup(sample.ID).AwaitResult();
            Assert.AreNotSame(sample, retrieved);
            Assert.AreEqual(sample, retrieved);
        }

        [TestMethod]
        public void ModifyRetrievedEntityDoesntModifyPersistedCopy()
        {
            U sample = Sample;
            TestObject.Insert(sample).AwaitResult();
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