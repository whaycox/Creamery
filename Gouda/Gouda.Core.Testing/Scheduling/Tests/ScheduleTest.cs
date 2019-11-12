using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Scheduling.Tests
{
    using Gouda.Template;
    using Implementation;

    [TestClass]
    public class ScheduleTest : TimeTemplate
    {
        private Schedule TestObject = new Schedule();

        private void AddNNodes(int numberOfNodes, int intervalInS)
        {
            for (int i = 0; i < numberOfNodes; i++)
            {
                TestObject.Add(i, TestTime);

                if (i < numberOfNodes - 1)
                    TestTime = TestTime.AddSeconds(intervalInS);
            }

        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(10, 1)]
        [DataRow(10, -1)]
        [DataRow(1000, 1)]
        [DataRow(1000, -1)]
        public void CanAddNIDs(int numberOfNodes, int intervalInS)
        {
            AddNNodes(numberOfNodes, intervalInS);
        }

        [TestMethod]
        public void CanAddOutOfOrder()
        {
            TestObject.Add(1, TestTime.AddSeconds(5));
            TestObject.Add(2, TestTime.AddSeconds(2));
            TestObject.Add(3, TestTime);

            List<int> ids = TestObject.Trim(TestTime.AddSeconds(10));

            Assert.AreEqual(3, ids.Count);
            Assert.AreEqual(3, ids[0]);
            Assert.AreEqual(2, ids[1]);
            Assert.AreEqual(1, ids[2]);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public void TrimReturnsAllOnOrBeforeTime(int numberOfNodes)
        {
            AddNNodes(numberOfNodes, 10);

            List<int> ids = TestObject.Trim(TestTime);

            Assert.AreEqual(numberOfNodes, ids.Count);
        }

        [TestMethod]
        public void TrimReturnsInCorrectOrder()
        {
            DateTimeOffset startingTime = TestTime;
            AddNNodes(100, 1);
            DateTimeOffset trimTime = startingTime.AddSeconds(50);

            List<int> ids = TestObject.Trim(trimTime);

            Assert.AreEqual(51, ids.Count);
            for (int i = 0; i < ids.Count; i++)
                Assert.AreEqual(i, ids[i]);
        }

        [TestMethod]
        public void TrimOnEmptyScheduleReturnsEmptyCollection()
        {
            List<int> ids = TestObject.Trim(TestTime);

            Assert.AreEqual(0, ids.Count);
        }

        [TestMethod]
        public async Task CanAddConcurrently()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                int id = i;
                DateTimeOffset scheduledTime = TestTime.AddSeconds(i);
                tasks.Add(new Task(() => TestObject.Add(id, scheduledTime)));
            }
            tasks.ForEach(task => task.Start());
            await Task.WhenAll(tasks);

            DateTimeOffset trimTime = TestTime.AddSeconds(50);

            List<int> ids = TestObject.Trim(trimTime);

            Assert.AreEqual(51, ids.Count);
            for (int i = 0; i < ids.Count; i++)
                Assert.AreEqual(i, ids[i]);
        }

    }
}
