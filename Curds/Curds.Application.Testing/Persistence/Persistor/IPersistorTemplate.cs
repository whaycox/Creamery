using Curds.Domain;
using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Curds.Application.Persistence.Persistor
{
    public abstract class IPersistorTemplate<T, U> : CronTemplate<T> where T : IPersistor<U> where U : BaseEntity
    {
        private const int ConcurrentIterations = 10;

        protected abstract U Sample { get; }

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
            U[] retrieved = TestObject.FetchAll().AwaitResult().ToArray();
            Assert.AreEqual(results.Length, retrieved.Length);
        }
        private void ConcurrentInsertHelper(int index, U[] results)
        {
            Debug.WriteLine($"Inserting with index {index}");
            results[index] = TestObject.Insert(Sample).AwaitResult();
        }
    }
}
