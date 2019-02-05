using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Curds.Persistence.Relationships
{
    public class OneToMany : CachedRelationship
    {
        private ConcurrentDictionary<int, HashSet<int>> Relationships = new ConcurrentDictionary<int, HashSet<int>>();

        public override void AddRelationship(int keyID, int relatedID) => Relationships.AddOrUpdate(keyID, StartingSet(relatedID), (k, v) => AddToSet(v, relatedID));
        private HashSet<int> StartingSet(int relatedID)
        {
            HashSet<int> toReturn = new HashSet<int>();
            toReturn.Add(relatedID);
            return toReturn;
        }
        private HashSet<int> AddToSet(HashSet<int> set, int relatedID)
        {
            set.Add(relatedID);
            return set;
        }

        public override void SeverRelationship(int keyID, int valueID)
        {
            throw new NotImplementedException();
        }
    }
}
