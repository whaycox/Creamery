using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistors.Template
{
    using Abstraction;

    public abstract class IPersistor<T, U> : Test<T> where T : IPersistor<U>
    {
        private const int ConcurrentIterations = 10;

        protected abstract U Sample { get; }

        [TestMethod]
        public async Task FetchAllWorksWhenEmpty()
        {
            foreach (U test in await TestObject.FetchAll())
                Assert.Fail();
        }

        [TestMethod]
        public async Task CanInsertNewItem()
        {
            Assert.AreEqual(0, await TestObject.Count());
            await TestObject.Insert(Sample);
            Assert.AreEqual(1, await TestObject.Count());
        }

        [TestMethod]
        public async Task CanConcurrentlyInsertNewItems()
        {
            U[] results = new U[ConcurrentIterations];
            Task[] tasks = new Task[ConcurrentIterations];
            for (int i = 0; i < ConcurrentIterations; i++)
            {
                int index = i;
                tasks[i] = ConcurrentInsertHelper(index, results);
            }
            Task.WaitAll(tasks);
            List<U> retrieved = await TestObject.FetchAll();
            Assert.AreEqual(results.Length, retrieved.Count);
        }
        private async Task ConcurrentInsertHelper(int index, U[] results)
        {
            Debug.WriteLine($"Inserting with index {index}");
            results[index] = await TestObject.Insert(Sample);
        }
    }
}
