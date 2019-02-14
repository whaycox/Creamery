using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace Curds.Application.Persistence
{
    public abstract class ISeedGeneratorTemplate<T> : TestTemplate<T> where T : ISeedGenerator
    {
        [TestMethod]
        public void StartsAtOne()
        {
            Assert.AreEqual(1, TestObject.Next);
        }

        [TestMethod]
        public void EachCallIsOneBigger()
        {
            int last = 0;
            for (int i = 0; i < 10; i++)
            {
                int fetched = TestObject.Next;
                Assert.AreEqual(last + 1, fetched);
                last = fetched;
            }
        }

        [TestMethod]
        public void ManyThreadsAllGetDifferentSeeds()
        {
            int threads = 15;
            int[] seeds = new int[threads];
            Task[] tasks = new Task[threads];
            for (int i = 0; i < 15; i++)
            {
                int index = i;
                Debug.WriteLine($"Adding at {index}");
                tasks[index] = Task.Factory.StartNew(() => seeds[index] = TestObject.Next);
            }
            Task.WaitAll(tasks);
            Assert.IsFalse(seeds.Where(s => s == 0).Any());
            Assert.AreEqual(threads, seeds.GroupBy(s => s).Count());
            Assert.AreEqual(120, seeds.Sum());
        }
    }
}
