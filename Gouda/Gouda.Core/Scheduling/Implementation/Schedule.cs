using System;
using System.Collections.Generic;

namespace Gouda.Scheduling.Implementation
{
    using Abstraction;
    using Domain;

    public class Schedule : ISchedule
    {
        private object Locker = new object();

        private ScheduleNode First { get; set; }
        private ScheduleNode Last { get; set; }

        public int Count { get; private set; }

        private void AddFirstNode(ScheduleNode newNode)
        {
            First = newNode;
            Last = newNode;
            Count++;
        }
        private void InsertAtEnd(ScheduleNode newNode)
        {
            ScheduleNode last = Last;
            last.Next = newNode;
            newNode.Previous = last;
            Last = newNode;
            Count++;
        }
        private void InsertBefore(ScheduleNode olderNeighbor, ScheduleNode newNode)
        {
            ScheduleNode previousNode = olderNeighbor.Previous;
            olderNeighbor.Previous = newNode;
            newNode.Next = olderNeighbor;
            newNode.Previous = previousNode;

            if (newNode.Previous == null)
                First = newNode;
            Count++;
        }

        public void Add(int checkID, DateTimeOffset scheduledTime)
        {
            lock (Locker)
            {
                ScheduleNode newNode = new ScheduleNode(checkID, scheduledTime);
                if (Count == 0)
                    AddFirstNode(newNode);
                else
                {
                    ScheduleNode olderNeighbor = SeekFromEnd(scheduledTime);
                    if (olderNeighbor == null)
                        InsertAtEnd(newNode);
                    else
                        InsertBefore(olderNeighbor, newNode);
                }
            }
        }
        private ScheduleNode SeekFromEnd(DateTimeOffset seekTime)
        {
            ScheduleNode searchedNode = Last;
            while (searchedNode.Previous != null && searchedNode.Previous.ScheduledTime > seekTime)
                searchedNode = searchedNode.Previous;

            if (searchedNode.ScheduledTime <= seekTime)
                return null;
            else
                return searchedNode;
        }

        public List<int> Trim(DateTimeOffset maxTime)
        {
            lock (Locker)
            {
                ScheduleNode lastEligible = SeekFromStart(maxTime);
                if (lastEligible == null)
                    return new List<int>();
                else
                {
                    TrimList(lastEligible);
                    return CollapseForward(lastEligible);
                }
            }
        }
        private ScheduleNode SeekFromStart(DateTimeOffset seekTime)
        {
            ScheduleNode searchedNode = First;
            while (searchedNode != null && searchedNode.Next != null && searchedNode.Next.ScheduledTime <= seekTime)
                searchedNode = searchedNode.Next;

            if (searchedNode == null || searchedNode.ScheduledTime > seekTime)
                return null;
            else
                return searchedNode;
        }
        private void TrimList(ScheduleNode trimmedNode)
        {
            ScheduleNode next = trimmedNode.Next;
            if (next != null)
            {
                next.Previous = null;
                First = next;
            }
        }
        private List<int> CollapseForward(ScheduleNode last)
        {
            List<int> ids = new List<int>();

            ScheduleNode currentNode = last;
            while (currentNode != null)
            {
                ids.Add(currentNode.CheckID);
                currentNode = currentNode.Previous;
            }

            ids.Reverse();
            return ids;
        }
    }
}
