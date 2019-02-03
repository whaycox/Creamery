using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Curds.Domain.Persistence.Relationships
{
    public class OneToMany : CachedRelationship
    {
        private ConcurrentDictionary<int, HashSet<int>> Relationships = new ConcurrentDictionary<int, HashSet<int>>();

        public override void AddRelationship(int keyID, int valueID) => Relationships.AddOrUpdate(keyID, StartingSet(valueID), (k, v) => AddToSet(v, valueID));
        private HashSet<int> StartingSet(int valueID)
        {
            HashSet<int> toReturn = new HashSet<int>();
            toReturn.Add(valueID);
            return toReturn;
        }
        private HashSet<int> AddToSet(HashSet<int> set, int valueID)
        {
            set.Add(valueID);
            return set;
        }

        public override void SeverRelationship(int keyID, int valueID)
        {
            throw new NotImplementedException();
        }
    }
}
