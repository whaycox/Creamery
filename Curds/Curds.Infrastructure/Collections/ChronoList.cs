using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Collections
{
    public class ChronoList<T>
    {
        private object Locker = new object();

        private ChronoNode First = null;
        private ChronoNode Last = null;

        public void AddNow(T item) => AddFirst(new ChronoNode(DateTimeOffset.MinValue, item));
        public void Add(DateTimeOffset time, T item)
        {
            lock (Locker)
            {
                ChronoNode nearestNeighbor = NearestLaterNodeFromEnd(time);
                ChronoNode newNode = new ChronoNode(time, item);
                if (nearestNeighbor == null)
                    AddLast(newNode);
                else
                    AddBefore(nearestNeighbor, newNode);
            }
        }

        public IEnumerable<T> Retrieve(DateTimeOffset time)
        {
            lock (Locker)
                return SeverAt(LastNodeInRangeFromStart(time));
        }

        private ChronoNode NearestLaterNodeFromEnd(DateTimeOffset seekTime)
        {
            ChronoNode currentNode = Last;
            if (currentNode == null || !NodeIsInRangeFromEnd(currentNode, seekTime))
                return null;
            while (currentNode.Previous != null && PreviousIsInRangeFromEnd(currentNode, seekTime))
                currentNode = currentNode.Previous;
            return currentNode;
        }
        private bool PreviousIsInRangeFromEnd(ChronoNode node, DateTimeOffset seekTime) => node.Previous == null ? false : NodeIsInRangeFromEnd(node.Previous, seekTime);
        private bool NodeIsInRangeFromEnd(ChronoNode node, DateTimeOffset seekTime) => node.ScheduledTime >= seekTime;

        private ChronoNode LastNodeInRangeFromStart(DateTimeOffset seekTime)
        {
            ChronoNode currentNode = First;
            if (currentNode == null || !NodeIsInRangeFromStart(currentNode, seekTime))
                return null;
            while (currentNode.Next != null && NextIsInRangeFromStart(currentNode, seekTime))
                currentNode = currentNode.Next;
            return currentNode;
        }
        private bool NextIsInRangeFromStart(ChronoNode node, DateTimeOffset seekTime) => node.Next == null ? false : NodeIsInRangeFromStart(node.Next, seekTime);
        private bool NodeIsInRangeFromStart(ChronoNode node, DateTimeOffset seekTime) => node.ScheduledTime <= seekTime;

        private void AddFirst(ChronoNode newNode)
        {
            lock (Locker)
            {
                ChronoNode currentFirst = First;
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
        private void AddLast(ChronoNode newNode)
        {

            lock (Locker)
            {
                ChronoNode currentLast = Last;
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
        private void AddBefore(ChronoNode targetNode, ChronoNode newNode)
        {
            lock (Locker)
            {
                ChronoNode ultimate = targetNode.Previous;
                newNode.Previous = ultimate;
                newNode.Next = targetNode;
                targetNode.Previous = newNode;

                if (newNode.Previous == null)
                    First = newNode;
            }
        }
        private void AddAfter(ChronoNode targetNode, ChronoNode newNode)
        {
            lock (Locker)
            {
                ChronoNode ultimate = targetNode.Next;
                targetNode.Next = newNode;
                newNode.Previous = targetNode;
                newNode.Next = ultimate;

                if (newNode.Next == null)
                    Last = newNode;
            }
        }
        private IEnumerable<T> SeverAt(ChronoNode node)
        {
            lock (Locker)
            {
                if (node == null)
                    return null;

                ChronoNode newFirst = node.Next;
                First = newFirst;
                if (newFirst == null)
                    Last = null;
                else
                    newFirst.Previous = null;

                return RollUpByPrevious(node);
            }
        }
        private IEnumerable<T> RollUpByPrevious(ChronoNode lastNode)
        {
            List<T> toReturn = new List<T>();
            ChronoNode currentNode = lastNode;
            while (currentNode.Previous != null)
            {
                toReturn.Add(currentNode.Value);
                currentNode = currentNode.Previous;
            }
            toReturn.Add(currentNode.Value);
            toReturn.Reverse();
            return toReturn;
        }

        private class ChronoNode
        {
            public ChronoNode Previous { get; set; }
            public ChronoNode Next { get; set; }

            public DateTimeOffset ScheduledTime { get; }
            public T Value { get; }

            public ChronoNode(DateTimeOffset scheduledTime, T value)
            {
                ScheduledTime = scheduledTime;
                Value = value;
            }

            public override string ToString() => $"{ScheduledTime}:{Value}";
        }
    }
}
