using System;
using System.Collections.Generic;

namespace Curds.Collections.Implementation
{
    using Abstraction;

    public class TrimmableLinkedList<TEntity>
    {
        private Dictionary<IDualLinkNode<TEntity>, DualLinkNode<TEntity>> Nodes { get; } = new Dictionary<IDualLinkNode<TEntity>, DualLinkNode<TEntity>>();

        public int Count => Nodes.Count;
        public IDualLinkNode<TEntity> First { get; private set; }
        public IDualLinkNode<TEntity> Last { get; private set; }

        private DualLinkNode<TEntity> FindNode(IDualLinkNode<TEntity> suppliedNode)
        {
            if (suppliedNode != null)
            {
                if (!Nodes.TryGetValue(suppliedNode, out DualLinkNode<TEntity> foundNode))
                    throw new InvalidOperationException("Unknown node supplied");
                return foundNode;
            }
            return null;
        }

        private void AddElement(DualLinkNode<TEntity> node)
        {
            Nodes.Add(node, node);
            if (First == null)
                First = node;
            if (Last == null)
                Last = node;
            RealignEnds();
        }
        private void RealignEnds()
        {
            while (First?.Previous != null)
                First = First.Previous;
            while (Last?.Next != null)
                Last = Last.Next;
        }

        public IDualLinkNode<TEntity> AddFirst(TEntity entity) => AddBefore(First, entity);
        public IDualLinkNode<TEntity> AddBefore(IDualLinkNode<TEntity> suppliedNode, TEntity entity)
        {
            DualLinkNode<TEntity> sourceNode = FindNode(suppliedNode);
            DualLinkNode<TEntity> newNode = new DualLinkNode<TEntity>(this, entity);
            if (sourceNode != null)
                newNode.AttachBefore(sourceNode);
            AddElement(newNode);

            return newNode;
        }

        public IDualLinkNode<TEntity> AddLast(TEntity entity) => AddAfter(Last, entity);
        public IDualLinkNode<TEntity> AddAfter(IDualLinkNode<TEntity> suppliedNode, TEntity entity)
        {
            DualLinkNode<TEntity> sourceNode = FindNode(suppliedNode);
            DualLinkNode<TEntity> newNode = new DualLinkNode<TEntity>(this, entity);
            if (sourceNode != null)
                newNode.AttachAfter(sourceNode);
            AddElement(newNode);

            return newNode;
        }

        public void PlaceFirst(IDualLinkNode<TEntity> node) => PlaceBefore(First, node);
        public void PlaceBefore(IDualLinkNode<TEntity> sourceNode, IDualLinkNode<TEntity> targetNode)
        {
            DualLinkNode<TEntity> source = FindNode(sourceNode);
            DualLinkNode<TEntity> target = FindNode(targetNode);
            if (source != target)
            {
                target.AttachBefore(source);
                RealignEnds();
            }
        }

        public void PlaceLast(IDualLinkNode<TEntity> node) => PlaceAfter(Last, node);
        public void PlaceAfter(IDualLinkNode<TEntity> sourceNode, IDualLinkNode<TEntity> targetNode)
        {
            DualLinkNode<TEntity> source = FindNode(sourceNode);
            DualLinkNode<TEntity> target = FindNode(targetNode);
            if (source != target)
            {
                target.AttachAfter(source);
                RealignEnds();
            }
        }

        public TEntity Remove(IDualLinkNode<TEntity> node)
        {
            DualLinkNode<TEntity> targetNode = FindNode(node);
            if (targetNode == First)
                First = targetNode.Next;
            if (targetNode == Last)
                Last = targetNode.Previous;
            targetNode.DetachCompletely();
            Nodes.Remove(node);
            RealignEnds();

            return targetNode.Value;
        }

        public List<TEntity> TrimBefore(IDualLinkNode<TEntity> node)
        {
            DualLinkNode<TEntity> trimNode = FindNode(node);
            DualLinkNode<TEntity> detached = trimNode.DetachBefore();
            First = trimNode;

            List<TEntity> trimmedValues = new List<TEntity>();
            IDualLinkNode<TEntity> currentNode = detached;
            while (currentNode != null)
            {
                Nodes.Remove(currentNode);
                trimmedValues.Add(currentNode.Value);
                currentNode = currentNode.Previous;
            }
            trimmedValues.Reverse();
            return trimmedValues;
        }
        public List<TEntity> TrimAfter(IDualLinkNode<TEntity> node)
        {
            DualLinkNode<TEntity> trimNode = FindNode(node);
            DualLinkNode<TEntity> detached = trimNode.DetachAfter();
            Last = trimNode;

            List<TEntity> trimmedValues = new List<TEntity>();
            IDualLinkNode<TEntity> currentNode = detached;
            while (currentNode != null)
            {
                Nodes.Remove(currentNode);
                trimmedValues.Add(currentNode.Value);
                currentNode = currentNode.Next;
            }
            return trimmedValues;
        }
    }
}
