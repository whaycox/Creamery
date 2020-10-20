using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Collections.Tests
{
    using Implementation;

    [TestClass]
    public class LinkedListNodeTest
    {
        private object TestParent = new object();
        private LinkedListNode<int> TestNodeA = null;
        private LinkedListNode<int> TestNodeB = null;
        private LinkedListNode<int> TestNodeC = null;

        private LinkedListNode<int> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestNodeA = new LinkedListNode<int>(TestParent);
            TestNodeB = new LinkedListNode<int>(TestParent);
            TestNodeC = new LinkedListNode<int>(TestParent);

            TestObject = new LinkedListNode<int>(TestParent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotSupplyNullParent()
        {
            new LinkedListNode<int>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotAttachBeforeNodeOfDifferentParent()
        {
            object otherParent = new object();
            LinkedListNode<int> otherNode = new LinkedListNode<int>(otherParent);

            TestObject.AttachBefore(otherNode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotAttachAfterNodeOfDifferentParent()
        {
            object otherParent = new object();
            LinkedListNode<int> otherNode = new LinkedListNode<int>(otherParent);

            TestObject.AttachAfter(otherNode);
        }

        [TestMethod]
        public void AttachBeforeSetsNextOfSource()
        {
            TestObject.AttachBefore(TestNodeA);

            Assert.AreSame(TestNodeA, TestObject.Next);
        }

        [TestMethod]
        public void AttachBeforeSetsPreviousOfSource()
        {
            TestNodeA.AttachBefore(TestNodeB);

            TestObject.AttachBefore(TestNodeB);

            Assert.AreSame(TestNodeA, TestObject.Previous);
        }

        [TestMethod]
        public void AttachBeforeSetsPreviousOfTarget()
        {
            TestObject.AttachBefore(TestNodeA);

            Assert.AreSame(TestObject, TestNodeA.Previous);
        }

        [TestMethod]
        public void AttachBeforeSetsNextOfTargetsPrevious()
        {
            TestNodeA.AttachBefore(TestNodeB);

            TestObject.AttachBefore(TestNodeB);

            Assert.AreSame(TestObject, TestNodeA.Next);
        }

        [TestMethod]
        public void AttachBeforeSetsNextOfSourcesPrevious()
        {
            TestNodeA.AttachBefore(TestNodeB);
            TestObject.AttachBefore(TestNodeB);

            TestObject.AttachBefore(TestNodeC);

            Assert.AreSame(TestNodeB, TestNodeA.Next);
        }

        [TestMethod]
        public void AttachBeforeSetsPreviousOfSourcesNext()
        {
            TestNodeA.AttachBefore(TestNodeB);
            TestObject.AttachBefore(TestNodeB);

            TestObject.AttachBefore(TestNodeC);

            Assert.AreSame(TestNodeA, TestNodeB.Previous);
        }

        [TestMethod]
        public void DetachBeforeUnsetsPreviousOfSource()
        {
            TestNodeA.AttachBefore(TestNodeB);
            TestObject.AttachBefore(TestNodeB);

            TestObject.DetachBefore();

            Assert.IsNull(TestObject.Previous);
        }

        [TestMethod]
        public void DetachBeforeUnsetsNextOfDetached()
        {
            TestNodeA.AttachBefore(TestNodeB);
            TestObject.AttachBefore(TestNodeB);

            TestObject.DetachBefore();

            Assert.IsNull(TestNodeA.Next);
        }

        [TestMethod]
        public void DetachBeforeReturnsDetached()
        {
            TestNodeA.AttachBefore(TestNodeB);
            TestObject.AttachBefore(TestNodeB);

            LinkedListNode<int> actual = TestObject.DetachBefore();

            Assert.AreSame(TestNodeA, actual);
        }

        [TestMethod]
        public void DetachBeforeReturnsNullIfNoRelationship()
        {
            TestObject.AttachBefore(TestNodeB);

            Assert.IsNull(TestObject.DetachBefore());
        }

        [TestMethod]
        public void AttachAfterSetsPreviousOfSource()
        {
            TestObject.AttachAfter(TestNodeA);

            Assert.AreSame(TestNodeA, TestObject.Previous);
        }

        [TestMethod]
        public void AttachAfterSetsNextOfSource()
        {
            TestNodeB.AttachAfter(TestNodeA);

            TestObject.AttachAfter(TestNodeA);

            Assert.AreSame(TestNodeB, TestObject.Next);
        }

        [TestMethod]
        public void AttachAfterSetsNextOfTarget()
        {
            TestObject.AttachAfter(TestNodeA);

            Assert.AreSame(TestObject, TestNodeA.Next);
        }

        [TestMethod]
        public void AttachAfterSetsPreviousOfTargetsNext()
        {
            TestNodeB.AttachAfter(TestNodeA);

            TestObject.AttachAfter(TestNodeA);

            Assert.AreSame(TestObject, TestNodeB.Previous);
        }

        [TestMethod]
        public void AttachAfterSetsNextOfSourcesPrevious()
        {
            TestNodeB.AttachAfter(TestNodeA);
            TestObject.AttachAfter(TestNodeA);

            TestObject.AttachAfter(TestNodeC);

            Assert.AreSame(TestNodeB, TestNodeA.Next);
        }

        [TestMethod]
        public void AttachAfterSetsPreviousOfSourcesNext()
        {
            TestNodeB.AttachAfter(TestNodeA);
            TestObject.AttachAfter(TestNodeA);

            TestObject.AttachAfter(TestNodeC);

            Assert.AreSame(TestNodeA, TestNodeB.Previous);
        }

        [TestMethod]
        public void DetachAfterUnsetsNextOfSource()
        {
            TestNodeB.AttachAfter(TestNodeA);
            TestObject.AttachAfter(TestNodeA);

            TestObject.DetachAfter();

            Assert.IsNull(TestObject.Next);
        }

        [TestMethod]
        public void DetachAfterUnsetsPreviousOfDetached()
        {
            TestNodeB.AttachAfter(TestNodeA);
            TestObject.AttachAfter(TestNodeA);

            TestObject.DetachAfter();

            Assert.IsNull(TestNodeB.Previous);
        }

        [TestMethod]
        public void DetachAfterReturnsDetached()
        {
            TestNodeB.AttachAfter(TestNodeA);
            TestObject.AttachAfter(TestNodeA);

            LinkedListNode<int> actual = TestObject.DetachAfter();

            Assert.AreSame(TestNodeB, actual);
        }

        [TestMethod]
        public void DetachAfterReturnsNullIfNoRelationship()
        {
            TestObject.AttachAfter(TestNodeA);

            Assert.IsNull(TestObject.DetachAfter());
        }
    }
}
