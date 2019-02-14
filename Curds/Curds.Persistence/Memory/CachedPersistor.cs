using Curds.Application.Persistence;
using Curds.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Memory
{
    public class CachedPersistor<T> : IPersistor<T> where T : Entity
    {
        private SeedGenerator Seed = new SeedGenerator();
        private Cache<T> Cache = new Cache<T>();

        public int Count => Cache.Count;

        public CachedPersistor()
            : this(null)
        { }

        public CachedPersistor(IEnumerable<T> startingSet)
        {
            if (startingSet != null)
                Cache.AddOrUpdate(startingSet);
        }

        public IEnumerable<T> FetchAll() => Cache.RetrieveAll();

        public T Lookup(int id) => Cache.Retrieve(id).Clone() as T;
        public IEnumerable<T> Lookup(IEnumerable<int> ids) => Cache.Retrieve(ids).Select(r => r.Clone() as T);

        public T Insert(T newEntity)
        {
            if (newEntity.ID != default(int))
                throw new InvalidOperationException("Cannot insert an entity that already has an ID");
            newEntity.ID = Seed.Next;
            Cache.Insert(newEntity.Clone() as T);
            return newEntity;
        }

        public void Update(int id, Func<T, T> updateDelegate)
        {
            T current = Lookup(id);
            int currentID = current.ID;
            T modified = updateDelegate(current);
            if (modified.ID != currentID)
                throw new InvalidOperationException("Cannot update an entity's ID");
            Cache.Update(id, modified);
        }

        public void Delete(int id) => Cache.Remove(id);
    }
}
