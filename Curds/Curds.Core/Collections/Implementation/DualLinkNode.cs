using System;

namespace Curds.Collections.Implementation
{
    using Abstraction;

    public class DualLinkNode<TEntity> : IDualLinkNode<TEntity>
    {
        private object Parent { get; set; }

        public TEntity Value { get; }

        public IDualLinkNode<TEntity> Previous => _previous;
        private DualLinkNode<TEntity> _previous = null;

        public IDualLinkNode<TEntity> Next => _next;
        private DualLinkNode<TEntity> _next = null;

        public DualLinkNode(
            object parent,
            TEntity value)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Value = value;
        }

        private void CollapseCurrent()
        {
            if (_previous != null)
                _previous._next = _next;
            if (_next != null)
                _next._previous = _previous;

            _previous = null;
            _next = null;
        }

        private void VerifyNodesHaveCommonParent(DualLinkNode<TEntity> node)
        {
            if (!ReferenceEquals(Parent, node.Parent))
                throw new InvalidOperationException("Cannot attach nodes with different parents");
        }

        public void DetachCompletely()
        {
            CollapseCurrent();
            Parent = null;
        }

        public void AttachBefore(DualLinkNode<TEntity> node)
        {
            VerifyNodesHaveCommonParent(node);
            CollapseCurrent();
            DualLinkNode<TEntity> targetPrevious = node._previous;
            node._previous = this;
            if (targetPrevious != null)
                targetPrevious._next = this;
            _previous = targetPrevious;
            _next = node;
        }

        public DualLinkNode<TEntity> DetachBefore()
        {
            DualLinkNode<TEntity> currentPrevious = _previous;
            _previous = null;
            if (currentPrevious != null)
                currentPrevious._next = null;
            return currentPrevious;
        }

        public void AttachAfter(DualLinkNode<TEntity> node)
        {
            VerifyNodesHaveCommonParent(node);
            CollapseCurrent();
            DualLinkNode<TEntity> targetNext = node._next;
            node._next = this;
            if (targetNext != null)
                targetNext._previous = this;
            _previous = node;
            _next = targetNext;
        }

        public DualLinkNode<TEntity> DetachAfter()
        {
            DualLinkNode<TEntity> currentNext = _next;
            _next = null;
            if (currentNext != null)
                currentNext._previous = null;
            return currentNext;
        }
    }
}
