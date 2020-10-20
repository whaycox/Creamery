using System;

namespace Curds.Collections.Implementation
{
    public class LinkedListNode<TEntity>
    {
        private object Parent { get; }

        public LinkedListNode<TEntity> Previous { get; private set; }
        public LinkedListNode<TEntity> Next { get; private set; }

        public TEntity Value { get; set; }

        public LinkedListNode(object parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        private void CollapseCurrent()
        {
            if (Previous != null)
                Previous.Next = Next;
            if (Next != null)
                Next.Previous = Previous;
        }

        private void VerifyNodesHaveCommonParent(LinkedListNode<TEntity> node)
        {
            if (!ReferenceEquals(Parent, node.Parent))
                throw new InvalidOperationException("Cannot attach nodes with different parents");
        }

        public void AttachBefore(LinkedListNode<TEntity> node)
        {
            VerifyNodesHaveCommonParent(node);
            CollapseCurrent();
            LinkedListNode<TEntity> targetPrevious = node.Previous;
            node.Previous = this;
            if (targetPrevious != null)
                targetPrevious.Next = this;
            Previous = targetPrevious;
            Next = node;
        }

        public LinkedListNode<TEntity> DetachBefore()
        {
            LinkedListNode<TEntity> currentPrevious = Previous;
            Previous = null;
            if (currentPrevious != null)
                currentPrevious.Next = null;
            return currentPrevious;
        }

        public void AttachAfter(LinkedListNode<TEntity> node)
        {
            VerifyNodesHaveCommonParent(node);
            CollapseCurrent();
            LinkedListNode<TEntity> targetNext = node.Next;
            node.Next = this;
            if (targetNext != null)
                targetNext.Previous = this;
            Previous = node;
            Next = targetNext;
        }

        public LinkedListNode<TEntity> DetachAfter()
        {
            LinkedListNode<TEntity> currentNext = Next;
            Next = null;
            if (currentNext != null)
                currentNext.Previous = null;
            return currentNext;
        }
    }
}
