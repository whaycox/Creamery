using Curds.Domain;
using Curds.Domain.DateTimes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Infrastructure.Collections
{
    public abstract class ChronoListTemplate<T> : TestTemplate<T> where T : ChronoList<int> 
    {
        [TestMethod]
        public void AddReturnsNewNode()
        {
            var node = TestObject.AddNow(5);
            Assert.AreEqual(Time.Fetch, node.ScheduledTime);
            Assert.AreEqual(5, node.Value);
        }

        [TestMethod]
        public void AddNowAddsToFrontOfLine()
        {
            for (int i = 0; i < 10; i++)
                TestObject.AddNow(i);
            List<int> retrieved = TestObject.Retrieve(DateTimeOffset.MinValue).ToList();
            Assert.AreEqual(10, retrieved.Count);
            for (int i = 0; i < 10; i++)
                Assert.AreEqual(9 - i, retrieved[i]);
        }

        [TestMethod]
        public void RetrieveOnlyReturnsEarlier()
        {
            Time.SetIncrement(TimeSpan.FromMinutes(1));
            for (int i = 0; i < 10; i++)
                TestObject.Add(Time.Fetch, i);

            Time.Reset();
            Time.SetIncrement(TimeSpan.FromMinutes(1.5));
            int currentExpected = 0;
            for (int i = 0; i < 7; i++)
            {
                var retrieved = TestObject.Retrieve(Time.Fetch).ToList();
                if (i % 2 == 0)
                    Assert.AreEqual(1, retrieved.Count);
                else
                    Assert.AreEqual(2, retrieved.Count);

                foreach (int currentRetrieved in retrieved)
                    Assert.AreEqual(currentExpected++, currentRetrieved);
            }
            Assert.AreEqual(10, currentExpected);
        }

        [TestMethod]
        public void CanAddInMiddle()
        {
            Time.SetIncrement(TimeSpan.FromMinutes(1));
            for (int i = 0; i < 10; i++)
                TestObject.Add(Time.Fetch, i);

            Time.Reset();
            Time.SetIncrement(TimeSpan.FromMinutes(5.5));
            TestObject.Add(Time.Fetch, 99);

            Time.Reset();
            Time.SetIncrement(TimeSpan.FromMinutes(6));
            var retrieved = TestObject.Retrieve(Time.Fetch).ToList();
            Assert.AreEqual(7, retrieved.Count);
            for (int i = 0; i < 7; i++)
            {
                if (i == 5)
                    Assert.AreEqual(99, retrieved[i]);
                else if (i == 6)
                    Assert.AreEqual(i - 1, retrieved[i]);
                else
                    Assert.AreEqual(i, retrieved[i]);
            }
        }
    }
}
