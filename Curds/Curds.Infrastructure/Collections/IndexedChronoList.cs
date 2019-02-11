using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Collections
{
    public class IndexedChronoList : ChronoList<int>
    {
        private Dictionary<int, ChronoNode> Index = new Dictionary<int, ChronoNode>();

        public ChronoNode this[int index]
        {
            get
            {
                if (!Index.TryGetValue(index, out ChronoNode node))
                    return null;
                return node;
            }
        }

        public override ChronoNode Add(DateTimeOffset time, int item)
        {
            lock (Locker)
            {
                ChronoNode added = base.Add(time, item);
                Index.Add(item, added);
                return added;
            }
        }

        public override IEnumerable<int> Retrieve(DateTimeOffset time)
        {
            lock(Locker)
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
