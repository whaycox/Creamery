using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using Curds.Application.Cron;
using System.Linq;

namespace Curds.Persistence.Relationships
{
    public class CronOneToMany
    {
        private ConcurrentDictionary<int, ConcurrentDictionary<int, ICronObject>> Relationships = new ConcurrentDictionary<int, ConcurrentDictionary<int, ICronObject>>();

        public IEnumerable<int> Filter(int definition, DateTime testTime) => Relationships[definition].Where(u => u.Value.Test(testTime)).Select(u => u.Key).ToList();

        public void AddRelationship(int keyID, int relatedID, ICronObject cronFilter) => 
            Relationships.AddOrUpdate(
                keyID, 
                StartingSet(relatedID, cronFilter), 
                (k, e) => AddCronRelationship(e, relatedID, cronFilter)
                );
        private ConcurrentDictionary<int, ICronObject> StartingSet(int relatedID, ICronObject cronFilter)
        {
            ConcurrentDictionary<int, ICronObject> toReturn = new ConcurrentDictionary<int, ICronObject>();
            toReturn.AddOrUpdate(relatedID, cronFilter, (e, v) => cronFilter);
            return toReturn;
        }
        private ConcurrentDictionary<int, ICronObject> AddCronRelationship(ConcurrentDictionary<int, ICronObject> relatedEntities, int relatedID, ICronObject cronFilter)
        {
            relatedEntities.AddOrUpdate(relatedID, cronFilter, (e, v) => cronFilter);
            return relatedEntities;
        }

        public void SeverRelationship(int keyID, int relatedID)
        {
            throw new NotImplementedException();
        }
    }
}
