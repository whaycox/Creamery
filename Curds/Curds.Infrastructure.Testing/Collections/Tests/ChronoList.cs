using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Curds.Domain.DateTimes;

namespace Curds.Infrastructure.Collections.Tests
{
    [TestClass]
    public class ChronoList
    {
        private MockDateTime Time = new MockDateTime();
        private ChronoList<int> TestList = new ChronoList<int>();

        [TestInitialize]
        public void Init()
        {
            MockDateTime.Reset();
        }

        [TestMethod]
        public void AddNowAddsToFrontOfLine()
        {
            for (int i = 0; i < 10; i++)
                TestList.AddNow(i);
            List<int> retrieved = TestList.Retrieve(DateTimeOffset.MinValue).ToList();
            Assert.AreEqual(10, retrieved.Count);
            for (int i = 0; i < 10; i++)
                Assert.AreEqual(9 - i, retrieved[i]);
        }

        [TestMethod]
        public void RetrieveOnlyReturnsEarlier()
        {
            MockDateTime.SetIncrement(TimeSpan.FromMinutes(1));
            for (int i = 0; i < 10; i++)
                TestList.Add(Time.Fetch, i);

            MockDateTime.Reset();
            MockDateTime.SetIncrement(TimeSpan.FromMinutes(1.5));
            int currentExpected = 0;
            for (int i = 0; i < 7; i++)
            {
                var retrieved = TestList.Retrieve(Time.Fetch).ToList();
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
            MockDateTime.SetIncrement(TimeSpan.FromMinutes(1));
            for (int i = 0; i < 10; i++)
                TestList.Add(Time.Fetch, i);

            MockDateTime.Reset();
            MockDateTime.SetIncrement(TimeSpan.FromMinutes(5.5));
            TestList.Add(Time.Fetch, 99);

            MockDateTime.Reset();
            MockDateTime.SetIncrement(TimeSpan.FromMinutes(6));
            var retrieved = TestList.Retrieve(Time.Fetch).ToList();
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
