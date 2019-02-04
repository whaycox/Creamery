using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using Curds.Application.Cron;

namespace Curds.Persistence.Relationships
{
    public class CronOneToMany
    {
        private ConcurrentDictionary<int, ConcurrentDictionary<int, ICronObject>> Relationships = new ConcurrentDictionary<int, ConcurrentDictionary<int, ICronObject>>();

        public void AddRelationship(int keyID, int relatedID, ICronObject cronFilter) => Relationships.AddOrUpdate(keyID, StartingSet(relatedID, cronFilter), (k, e) => AddCronRelationship(e, relatedID, cronFilter));
        private ConcurrentDictionary<int, ICronObject> StartingSet(int relatedID, ICronObject cronFilter)
        {
            throw new NotImplementedException();
        }
        private ConcurrentDictionary<int, ICronObject> AddCronRelationship(ConcurrentDictionary<int, ICronObject> relatedEntities, int relatedID, ICronObject cronFilter)
        {
            throw new NotImplementedException();
        }

        public void SeverRelationship(int keyID, int relatedID)
        {
            throw new NotImplementedException();
        }
    }
}
