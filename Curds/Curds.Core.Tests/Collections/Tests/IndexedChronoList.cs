using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Collections.Tests
{
    [TestClass]
    public class IndexedChronoList : Template.ChronoList<Implementation.IndexedChronoList>
    {
        private Implementation.IndexedChronoList _obj = new Implementation.IndexedChronoList();
        protected override Implementation.IndexedChronoList TestObject => _obj;

        [TestMethod]
        public void MissingIndexReturnsNull()
        {
            Assert.IsNull(TestObject[-1]);
        }

        [TestMethod]
        public void AddedNodesAreAccessibleByIndex()
        {
            TestObject.AddNow(5);
            Assert.AreEqual(5, TestObject[5].Value);
            Assert.AreEqual(MockTime.Fetch, TestObject[5].ScheduledTime);

            DateTimeOffset testTime = MockTime.Fetch.AddDays(1);
            TestObject.Add(testTime, 10);
            Assert.AreEqual(10, TestObject[10].Value);
            Assert.AreEqual(testTime, TestObject[10].ScheduledTime);
        }

        [TestMethod]
        public void RetrievedNodesArentInIndexAnymore()
        {
            TestObject.AddNow(5);
            Assert.IsNotNull(TestObject[5]);

            List<int> retrieved = TestObject.Retrieve(MockTime.Fetch).ToList();
            Assert.AreEqual(1, retrieved.Count);
            Assert.AreEqual(5, retrieved[0]);
            Assert.IsNull(TestObject[5]);
        }

        [TestMethod]
        public void CanRemoveNode()
        {
            TestObject.AddNow(5);
            Assert.IsNotNull(TestObject[5]);
            TestObject.Remove(5);
            Assert.IsNull(TestObject[5]);
        }
    }
}
