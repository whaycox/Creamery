using System;
using System.Collections.Generic;

namespace Curds.Collections.Implementation
{
    public class ChronoList<T>
    {
        protected object Locker = new object();

        private ChronoNode<T> First = null;
        private ChronoNode<T> Last = null;

        public ChronoNode<T> AddNow(T item) => Add(DateTimeOffset.MinValue, item);
        public virtual ChronoNode<T> Add(DateTimeOffset time, T item)
        {
            lock (Locker)
            {
                ChronoNode<T> nearestNeighbor = NearestLaterNodeFromEnd(time);
                ChronoNode<T> newNode = new ChronoNode<T>(time, item);
                if (nearestNeighbor == null)
                    AddLast(newNode);
                else
                    AddBefore(nearestNeighbor, newNode);
                return newNode;
            }
        }

        public virtual IEnumerable<T> Retrieve(DateTimeOffset time)
        {
            lock (Locker)
                return SeverAt(LastNodeInRangeFromStart(time));
        }

        private ChronoNode<T> NearestLaterNodeFromEnd(DateTimeOffset seekTime)
        {
            ChronoNode<T> currentNode = Last;
            if (currentNode == null || !NodeIsInRangeFromEnd(currentNode, seekTime))
                return null;
            while (currentNode.Previous != null && PreviousIsInRangeFromEnd(currentNode, seekTime))
                currentNode = currentNode.Previous;
            return currentNode;
        }
        private bool PreviousIsInRangeFromEnd(ChronoNode<T> node, DateTimeOffset seekTime) => node.Previous == null ? false : NodeIsInRangeFromEnd(node.Previous, seekTime);
        private bool NodeIsInRangeFromEnd(ChronoNode<T> node, DateTimeOffset seekTime) => node.ScheduledTime >= seekTime;

        private ChronoNode<T> LastNodeInRangeFromStart(DateTimeOffset seekTime)
        {
            ChronoNode<T> currentNode = First;
            if (currentNode == null || !NodeIsInRangeFromStart(currentNode, seekTime))
                return null;
            while (currentNode.Next != null && NextIsInRangeFromStart(currentNode, seekTime))
                currentNode = currentNode.Next;
            return currentNode;
        }
        private bool NextIsInRangeFromStart(ChronoNode<T> node, DateTimeOffset seekTime) => node.Next == null ? false : NodeIsInRangeFromStart(node.Next, seekTime);
        private bool NodeIsInRangeFromStart(ChronoNode<T> node, DateTimeOffset seekTime) => node.ScheduledTime <= seekTime;

        private void AddFirst(ChronoNode<T> newNode)
        {
            lock (Locker)
            {
                ChronoNode<T> currentFirst = First;
                if (currentFirst == null)
                {
                    First = newNode;
                    Last = newNode;
                }
                else
                {
                    newNode.Next = currentFirst;
                    currentFirst.Previous = newNode;
                    First = newNode;
                }
            }
        }
        private void AddLast(ChronoNode<T> newNode)
        {

            lock (Locker)
            {
                ChronoNode<T> currentLast = Last;
                if (currentLast == null)
                {
                    First = newNode;
                    Last = newNode;
                }
                else
                {
                    currentLast.Next = newNode;
                    newNode.Previous = currentLast;
                    Last = newNode;
                }
            }
        }
        private void AddBefore(ChronoNode<T> targetNode, ChronoNode<T> newNode)
        {
            lock (Locker)
            {
                ChronoNode<T> ultimate = targetNode.Previous;
                newNode.Previous = ultimate;
                newNode.Next = targetNode;
                targetNode.Previous = newNode;

                if (newNode.Previous == null)
                    First = newNode;
            }
        }
        private void AddAfter(ChronoNode<T> targetNode, ChronoNode<T> newNode)
        {
            lock (Locker)
            {
                ChronoNode<T> ultimate = targetNode.Next;
                targetNode.Next = newNode;
                newNode.Previous = targetNode;
                newNode.Next = ultimate;

                if (newNode.Next == null)
                    Last = newNode;
            }
        }
        private IEnumerable<T> SeverAt(ChronoNode<T> node)
        {
            lock (Locker)
            {
                if (node == null)
                    return new List<T>();

                ChronoNode<T> newFirst = node.Next;
                First = newFirst;
                if (newFirst == null)
                    Last = null;
                else
                    newFirst.Previous = null;

                return RollUpByPrevious(node);
            }
        }
        private IEnumerable<T> RollUpByPrevious(ChronoNode<T> lastNode)
        {
            List<T> toReturn = new List<T>();
            ChronoNode<T> currentNode = lastNode;
            while (currentNode.Previous != null)
            {
                toReturn.Add(currentNode.Value);
                currentNode = currentNode.Previous;
            }
            toReturn.Add(currentNode.Value);
            toReturn.Reverse();
            return toReturn;
        }

        protected void Remove(ChronoNode<T> node)
        {
            lock (Locker)
            {
                ChronoNode<T> prev = node.Previous;
                ChronoNode<T> next = node.Next;

                if (node == Last)
                    Last = prev;
                if (node == First)
                    First = next;

                if (prev != null)
                    prev.Next = next;
                if (next != null)
                    next.Previous = prev;
            }
        }
    }
}
