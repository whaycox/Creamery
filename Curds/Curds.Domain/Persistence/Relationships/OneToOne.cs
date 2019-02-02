using System;
using System.Collections.Concurrent;
using System.Text;
using System.Diagnostics;

namespace Curds.Domain.Persistence.Relationships
{
    public class OneToOne : CachedRelationship
    {
        private ConcurrentDictionary<int, int> Collection = new ConcurrentDictionary<int, int>();

        public override void AddRelationship(int keyID, int valueID) => Collection.AddOrUpdate(keyID, valueID, (e, v) => valueID);

        public override void SeverRelationship(int keyID, int valueID)
        {
            int removedValue;
            Collection.TryRemove(keyID, out removedValue);
            if (removedValue != valueID)
                Trace.TraceWarning($"Expected to sever a relationship between {keyID} and {valueID} but got {removedValue} instead");
        }
    }
}
