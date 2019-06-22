using System;
using System.Collections.Generic;

namespace Curds.Collections.Implementation
{
    public class IndexedChronoList : ChronoList<int>
    {
        private Dictionary<int, ChronoNode<int>> Index = new Dictionary<int, ChronoNode<int>>();

        public ChronoNode<int> this[int index]
        {
            get
            {
                if (!Index.TryGetValue(index, out ChronoNode<int> node))
                    return null;
                return node;
            }
        }

        public bool ContainsIndex(int index) => Index.ContainsKey(index);

        public override ChronoNode<int> Add(DateTimeOffset time, int item)
        {
            if (Index.ContainsKey(item))
                throw new InvalidOperationException("Cannot add a duplicate item");

            lock (Locker)
            {
                ChronoNode<int> added = base.Add(time, item);
                Index.Add(item, added);
                return added;
            }
        }

        public override IEnumerable<int> Retrieve(DateTimeOffset time)
        {
            lock (Locker)
            {
                IEnumerable<int> retrieved = base.Retrieve(time);
                foreach (int key in retrieved)
                    Index.Remove(key);
                return retrieved;
            }
        }

        public void Remove(int index)
        {
            lock (Locker)
            {
                if (!Index.ContainsKey(index))
                    throw new KeyNotFoundException($"No node was found with index {index}");
                else
                {
                    Remove(Index[index]);
                    Index.Remove(index);
                }
            }
        }
    }
}
