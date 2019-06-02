using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Collections.Template
{
    public abstract class ChronoList<T> : Test<T> where T : Implementation.ChronoList<int> 
    {
        [TestMethod]
        public void AddReturnsNewNode()
        {
            var node = TestObject.AddNow(5);
            Assert.AreEqual(MockTime.Fetch, node.ScheduledTime);
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
            for (int i = 0; i < 10; i++)
            {
                MockTime.SetPointInTime(MockTime.Fetch.AddMinutes(1));
                TestObject.Add(MockTime.Fetch, i);
            }

            MockTime.Reset();
            int currentExpected = 0;
            for (int i = 0; i < 7; i++)
            {
                MockTime.SetPointInTime(MockTime.Fetch.AddMinutes(1.5));
                var retrieved = TestObject.Retrieve(MockTime.Fetch).ToList();
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
            for (int i = 0; i < 10; i++)
            {
                MockTime.SetPointInTime(MockTime.Fetch.AddMinutes(1));
                TestObject.Add(MockTime.Fetch, i);
            }

            MockTime.Reset();
            MockTime.SetPointInTime(MockTime.Fetch.AddMinutes(5.5));
            TestObject.Add(MockTime.Fetch, 99);

            MockTime.Reset();
            MockTime.SetPointInTime(MockTime.Fetch.AddMinutes(6));
            var retrieved = TestObject.Retrieve(MockTime.Fetch).ToList();
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
