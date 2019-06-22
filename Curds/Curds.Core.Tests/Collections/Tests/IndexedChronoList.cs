using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Collections.Tests
{
    [TestClass]
    public class IndexedChronoList : Template.ChronoList<Implementation.IndexedChronoList>
    {
        private const int TestValue = 17;
        private const int OtherValue = 3;

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
            TestObject.AddNow(TestValue);
            Assert.AreEqual(TestValue, TestObject[TestValue].Value);
            Assert.AreEqual(MockTime.Fetch, TestObject[TestValue].ScheduledTime);

            DateTimeOffset testTime = MockTime.Fetch.AddDays(1);
            TestObject.Add(testTime, OtherValue);
            Assert.AreEqual(OtherValue, TestObject[OtherValue].Value);
            Assert.AreEqual(testTime, TestObject[OtherValue].ScheduledTime);
        }

        [TestMethod]
        public void CannotAddDuplicateIndex()
        {
            TestObject.AddNow(TestValue);
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.AddNow(TestValue));
        }

        [TestMethod]
        public void CanTellIfIndexIsAdded()
        {
            Assert.IsFalse(TestObject.ContainsIndex(TestValue));
            TestObject.AddNow(TestValue);
            Assert.IsTrue(TestObject.ContainsIndex(TestValue));
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
