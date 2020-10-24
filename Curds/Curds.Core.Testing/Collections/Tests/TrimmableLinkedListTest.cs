using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Collections.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class TrimmableLinkedListTest
    {
        private int TestInt = 17;

        private static IEnumerable<object[]> TestNodeCounts => new List<object[]>
        {
            { new object[] { 1 } },
            { new object[] { 10 } },
            { new object[] { 100 } },
            { new object[] { 1000 } },
            { new object[] { 10000 } },
        };

        private static IEnumerable<object[]> TestTrimNodeCounts => new List<object[]>
        {
            { new object[] { 1, 0 } },
            { new object[] { 10, 1 } },
            { new object[] { 10, 4 } },
            { new object[] { 10, 8 } },
            { new object[] { 100, 0 } },
            { new object[] { 100, 25 } },
            { new object[] { 100, 99 } },
            { new object[] { 1000, 100 } },
            { new object[] { 1000, 333 } },
            { new object[] { 1000, 998 } },
            { new object[] { 10000, 8000 } },
        };

        private TrimmableLinkedList<int> TestObject = new TrimmableLinkedList<int>();

        private void VerifyTestObjectOrdering(int expectedNodes, List<IDualLinkNode<int>> nodeReference)
        {
            Assert.AreEqual(expectedNodes, TestObject.Count);

            IDualLinkNode<int> currentNode = TestObject.First;
            for (int i = 0; i < expectedNodes; i++)
            {
                if (i == 0)
                    Assert.IsNull(currentNode.Previous);
                else
                    Assert.AreSame(nodeReference[i - 1], currentNode.Previous);

                Assert.AreEqual(i, currentNode.Value);

                if (i == expectedNodes - 1)
                    Assert.IsNull(currentNode.Next);
                else
                    Assert.AreSame(nodeReference[i + 1], currentNode.Next);

                currentNode = currentNode.Next;
            }
        }

        [TestMethod]
        public void AddFirstReturnsExpectedType()
        {
            Assert.IsInstanceOfType(TestObject.AddFirst(TestInt), typeof(DualLinkNode<int>));
        }

        [TestMethod]
        public void AddFirstReturnsNodeWithValue()
        {
            IDualLinkNode<int> actual = TestObject.AddFirst(TestInt);

            Assert.AreEqual(TestInt, actual.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddFirstSetsFirstToReturned(int entities)
        {
            for (int i = 0; i < entities; i++)
                Assert.AreSame(TestObject.AddFirst(i), TestObject.First);
        }

        [TestMethod]
        public void AddFirstAttachesBeforeCurrentFirst()
        {
            IDualLinkNode<int> original = TestObject.AddFirst(TestInt);

            IDualLinkNode<int> actual = TestObject.AddFirst(TestInt);

            Assert.AreSame(original, actual.Next);
            Assert.AreSame(actual, original.Previous);
        }

        [TestMethod]
        public void AddFirstSetsLastToFirstElement()
        {
            IDualLinkNode<int> original = TestObject.AddFirst(TestInt);

            IDualLinkNode<int> actual = TestObject.AddFirst(TestInt);

            Assert.AreSame(TestObject.Last, original);
            Assert.AreSame(TestObject.First, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddFirstIncrementsCount(int entities)
        {
            for (int i = 0; i < entities; i++)
            {
                TestObject.AddFirst(i);
                Assert.AreEqual(i + 1, TestObject.Count);
            }

            Assert.AreEqual(entities, TestObject.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddFirstPreservesLinking(int entities)
        {
            List<IDualLinkNode<int>> nodeReference = new List<IDualLinkNode<int>>();

            for (int i = entities - 1; i >= 0; i--)
                nodeReference.Add(
                    TestObject.AddFirst(i));

            nodeReference.Reverse();
            VerifyTestObjectOrdering(entities, nodeReference);
        }

        [TestMethod]
        public void AddBeforeReturnsExpectedType()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);

            Assert.IsInstanceOfType(TestObject.AddBefore(first, TestInt), typeof(DualLinkNode<int>));
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddBeforeIncrementsCount(int entities)
        {
            IDualLinkNode<int> startNode = TestObject.AddFirst(TestInt);

            for (int i = 1; i <= entities; i++)
            {
                TestObject.AddBefore(startNode, i);
                Assert.AreEqual(i + 1, TestObject.Count);
            }

            Assert.AreEqual(entities + 1, TestObject.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddBeforePreservesLinking(int entities)
        {
            List<IDualLinkNode<int>> nodeReference = new List<IDualLinkNode<int>>();
            IDualLinkNode<int> startNode = TestObject.AddFirst(entities);

            for (int i = 0; i < entities; i++)
                nodeReference.Add(
                    TestObject.AddBefore(startNode, i));
            nodeReference.Add(startNode);

            VerifyTestObjectOrdering(entities + 1, nodeReference);
        }

        [TestMethod]
        public void AddBeforeReturnsNodeWithValue()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);

            IDualLinkNode<int> actual = TestObject.AddBefore(first, TestInt);

            Assert.AreEqual(TestInt, actual.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddBeforeUnknownNodeThrows()
        {
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.AddBefore(unknownNode, TestInt);
        }

        [TestMethod]
        public void AddLastReturnsExpectedType()
        {
            Assert.IsInstanceOfType(TestObject.AddLast(TestInt), typeof(DualLinkNode<int>));
        }

        [TestMethod]
        public void AddLastReturnsNodeWithValue()
        {
            IDualLinkNode<int> actual = TestObject.AddLast(TestInt);

            Assert.AreEqual(TestInt, actual.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddLastSetsLastToReturned(int entities)
        {
            for (int i = 0; i < entities; i++)
                Assert.AreSame(TestObject.AddLast(i), TestObject.Last);
        }

        [TestMethod]
        public void AddLastAttachesAfterCurrentLast()
        {
            IDualLinkNode<int> original = TestObject.AddLast(TestInt);

            IDualLinkNode<int> actual = TestObject.AddLast(TestInt);

            Assert.AreSame(original, actual.Previous);
            Assert.AreSame(actual, original.Next);
        }

        [TestMethod]
        public void AddLastSetsFirstToFirstElement()
        {
            IDualLinkNode<int> original = TestObject.AddLast(TestInt);

            IDualLinkNode<int> actual = TestObject.AddLast(TestInt);

            Assert.AreSame(TestObject.First, original);
            Assert.AreSame(TestObject.Last, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddLastIncrementsCount(int entities)
        {
            for (int i = 0; i < entities; i++)
            {
                TestObject.AddLast(i);
                Assert.AreEqual(i + 1, TestObject.Count);
            }

            Assert.AreEqual(entities, TestObject.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddLastPreservesLinking(int entities)
        {
            List<IDualLinkNode<int>> nodeReference = new List<IDualLinkNode<int>>();

            for (int i = 0; i < entities; i++)
                nodeReference.Add(
                    TestObject.AddLast(i));

            VerifyTestObjectOrdering(entities, nodeReference);
        }

        [TestMethod]
        public void AddAfterReturnsExpectedType()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);

            Assert.IsInstanceOfType(TestObject.AddAfter(first, TestInt), typeof(DualLinkNode<int>));
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddAfterIncrementsCount(int entities)
        {
            IDualLinkNode<int> startNode = TestObject.AddLast(TestInt);

            for (int i = 1; i <= entities; i++)
            {
                TestObject.AddAfter(startNode, i);
                Assert.AreEqual(i + 1, TestObject.Count);
            }

            Assert.AreEqual(entities + 1, TestObject.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestNodeCounts))]
        public void AddAfterPreservesLinking(int entities)
        {
            List<IDualLinkNode<int>> nodeReference = new List<IDualLinkNode<int>>();
            IDualLinkNode<int> startNode = TestObject.AddFirst(0);

            for (int i = entities; i > 0; i--)
                nodeReference.Add(
                    TestObject.AddAfter(startNode, i));
            nodeReference.Add(startNode);
            nodeReference.Reverse();

            VerifyTestObjectOrdering(entities + 1, nodeReference);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddAfterUnknownNodeThrows()
        {
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.AddAfter(unknownNode, TestInt);
        }

        [TestMethod]
        public void PlaceFirstSetsNodeAsFirst()
        {
            TestObject.AddFirst(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.PlaceFirst(testNode);

            Assert.AreSame(testNode, TestObject.First);
        }

        [TestMethod]
        public void PlaceFirstCanHandleFirstNode()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.PlaceFirst(testNode);
        }

        [TestMethod]
        public void PlaceFirstSetsNewLast()
        {
            IDualLinkNode<int> newLast = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.PlaceFirst(testNode);

            Assert.AreSame(newLast, TestObject.Last);
        }

        [TestMethod]
        public void PlaceFirstMaintainsLinks()
        {
            IDualLinkNode<int> newLast = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.PlaceFirst(testNode);

            Assert.IsNull(testNode.Previous);
            Assert.AreSame(newLast, testNode.Next);
            Assert.AreSame(testNode, newLast.Previous);
            Assert.IsNull(newLast.Next);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceFirstWithUnknownNodeThrows()
        {
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.PlaceFirst(unknownNode);
        }

        [TestMethod]
        public void PlaceBeforeMaintainsLinks()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> last = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddAfter(last, TestInt);

            TestObject.PlaceBefore(last, testNode);

            Assert.AreSame(testNode, first.Next);
            Assert.AreSame(first, testNode.Previous);
            Assert.AreSame(last, testNode.Next);
            Assert.AreSame(testNode, last.Previous);
        }

        [TestMethod]
        public void PlaceBeforeSetsFirst()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.PlaceBefore(first, testNode);

            Assert.AreSame(testNode, TestObject.First);
        }

        [TestMethod]
        public void PlaceBeforeSetsLast()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.PlaceBefore(first, testNode);

            Assert.AreSame(first, TestObject.Last);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceBeforeWithUnknownSourceThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.PlaceBefore(unknownNode, testNode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceBeforeWithUnknownTargetThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.PlaceBefore(testNode, unknownNode);
        }

        [TestMethod]
        public void PlaceLastSetsNodeAsLast()
        {
            TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.PlaceLast(testNode);

            Assert.AreSame(testNode, TestObject.Last);
        }

        [TestMethod]
        public void PlaceLastCanHandleLastNode()
        {
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.PlaceLast(testNode);
        }

        [TestMethod]
        public void PlaceLastSetsNewFirst()
        {
            IDualLinkNode<int> newFirst = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.PlaceLast(testNode);

            Assert.AreSame(newFirst, TestObject.First);
        }

        [TestMethod]
        public void PlaceLastMaintainsLinks()
        {
            IDualLinkNode<int> newFirst = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.PlaceLast(testNode);

            Assert.IsNull(newFirst.Previous);
            Assert.AreSame(testNode, newFirst.Next);
            Assert.AreSame(newFirst, testNode.Previous);
            Assert.IsNull(testNode.Next);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceLastWithUnknownNodeThrows()
        {
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.PlaceLast(unknownNode);
        }

        [TestMethod]
        public void PlaceAfterMaintainsLinks()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> last = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddBefore(first, TestInt);

            TestObject.PlaceAfter(first, testNode);

            Assert.AreSame(testNode, first.Next);
            Assert.AreSame(first, testNode.Previous);
            Assert.AreSame(last, testNode.Next);
            Assert.AreSame(testNode, last.Previous);
        }

        [TestMethod]
        public void PlaceAfterSetsLast()
        {
            IDualLinkNode<int> last = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.PlaceAfter(last, testNode);

            Assert.AreSame(testNode, TestObject.Last);
        }

        [TestMethod]
        public void PlaceAfterSetsFirst()
        {
            IDualLinkNode<int> last = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.PlaceAfter(last, testNode);

            Assert.AreSame(last, TestObject.First);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceAfterWithUnknownSourceThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.PlaceAfter(unknownNode, testNode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceAfterWithUnknownTargetThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.PlaceAfter(testNode, unknownNode);
        }

        [TestMethod]
        public void RemoveReturnsNodeValue()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            int actual = TestObject.Remove(testNode);

            Assert.AreEqual(TestInt, actual);
        }

        [TestMethod]
        public void RemoveDecrementsCount()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.Remove(testNode);

            Assert.AreEqual(0, TestObject.Count);
        }

        [TestMethod]
        public void RemoveNullsNodeRelationships()
        {
            TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);
            TestObject.AddLast(TestInt);

            TestObject.Remove(testNode);

            Assert.IsNull(testNode.Previous);
            Assert.IsNull(testNode.Next);
        }

        [TestMethod]
        public void RemoveConnectsNulledRelationships()
        {
            IDualLinkNode<int> first = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);
            IDualLinkNode<int> last = TestObject.AddLast(TestInt);

            TestObject.Remove(testNode);

            Assert.IsNull(first.Previous);
            Assert.AreSame(last, first.Next);
            Assert.AreSame(first, last.Previous);
            Assert.IsNull(last.Next);
        }

        [TestMethod]
        public void RemoveFirstSetsNewFirst()
        {
            IDualLinkNode<int> first = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);

            TestObject.Remove(testNode);

            Assert.AreSame(first, TestObject.First);
        }

        [TestMethod]
        public void RemoveLastSetsNewLast()
        {
            IDualLinkNode<int> last = TestObject.AddLast(TestInt);
            IDualLinkNode<int> testNode = TestObject.AddLast(TestInt);

            TestObject.Remove(testNode);

            Assert.AreSame(last, TestObject.Last);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveUnknownNodeThrows()
        {
            DualLinkNode<int> unknownNode = new DualLinkNode<int>(TestObject, TestInt);

            TestObject.Remove(unknownNode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddBeforeRemovedNodeThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            TestObject.Remove(testNode);

            TestObject.AddBefore(testNode, TestInt);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddAfterRemovedNodeThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            TestObject.Remove(testNode);

            TestObject.AddAfter(testNode, TestInt);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceBeforeRemovedNodeThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> removedNode = TestObject.AddFirst(TestInt);
            TestObject.Remove(removedNode);

            TestObject.PlaceBefore(removedNode, testNode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaceAfterRemovedNodeThrows()
        {
            IDualLinkNode<int> testNode = TestObject.AddFirst(TestInt);
            IDualLinkNode<int> removedNode = TestObject.AddFirst(TestInt);
            TestObject.Remove(removedNode);

            TestObject.PlaceAfter(removedNode, testNode);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestTrimNodeCounts))]
        public void TrimBeforeReturnsExpectedNodes(int nodeCount, int trimIndex)
        {
            List<int> expectedReturns = new List<int>();
            IDualLinkNode<int> trimNode = null;
            for (int i = 0; i < nodeCount; i++)
            {
                IDualLinkNode<int> testNode = TestObject.AddLast(i);
                if (i < trimIndex)
                    expectedReturns.Add(i);
                else if (i == trimIndex)
                    trimNode = testNode;
            }

            List<int> actual = TestObject.TrimBefore(trimNode);

            CollectionAssert.AreEqual(expectedReturns, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestTrimNodeCounts))]
        public void TrimBeforeDecrementsCount(int nodeCount, int trimIndex)
        {
            IDualLinkNode<int> trimNode = null;
            for (int i = 0; i < nodeCount; i++)
            {
                IDualLinkNode<int> testNode = TestObject.AddLast(i);
                if (i == trimIndex)
                    trimNode = testNode;
            }

            List<int> actual = TestObject.TrimBefore(trimNode);

            Assert.AreEqual(nodeCount - actual.Count, TestObject.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestTrimNodeCounts))]
        public void TrimBeforeSetsTrimNodeAsFirst(int nodeCount, int trimIndex)
        {
            IDualLinkNode<int> trimNode = null;
            for (int i = 0; i < nodeCount; i++)
            {
                IDualLinkNode<int> testNode = TestObject.AddLast(i);
                if (i == trimIndex)
                    trimNode = testNode;
            }

            TestObject.TrimBefore(trimNode);

            Assert.AreSame(trimNode, TestObject.First);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestTrimNodeCounts))]
        public void TrimAfterReturnsExpectedNodes(int nodeCount, int trimIndex)
        {
            List<int> expectedReturns = new List<int>();
            IDualLinkNode<int> trimNode = null;
            for (int i = 0; i < nodeCount; i++)
            {
                IDualLinkNode<int> testNode = TestObject.AddLast(i);
                if (i > trimIndex)
                    expectedReturns.Add(i);
                else if (i == trimIndex)
                    trimNode = testNode;
            }

            List<int> actual = TestObject.TrimAfter(trimNode);

            CollectionAssert.AreEqual(expectedReturns, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestTrimNodeCounts))]
        public void TrimAfterDecrementsCount(int nodeCount, int trimIndex)
        {
            IDualLinkNode<int> trimNode = null;
            for (int i = 0; i < nodeCount; i++)
            {
                IDualLinkNode<int> testNode = TestObject.AddLast(i);
                if (i == trimIndex)
                    trimNode = testNode;
            }

            List<int> actual = TestObject.TrimAfter(trimNode);

            Assert.AreEqual(nodeCount - actual.Count, TestObject.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestTrimNodeCounts))]
        public void TrimAfterSetsTrimNodeAsLast(int nodeCount, int trimIndex)
        {
            IDualLinkNode<int> trimNode = null;
            for (int i = 0; i < nodeCount; i++)
            {
                IDualLinkNode<int> testNode = TestObject.AddLast(i);
                if (i == trimIndex)
                    trimNode = testNode;
            }

            TestObject.TrimAfter(trimNode);

            Assert.AreSame(trimNode, TestObject.Last);
        }
    }
}
