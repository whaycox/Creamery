using Curds.Domain.Persistence;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Curds.Persistence
{
    public class Cache<T> where T : Entity
    {
        private ConcurrentDictionary<int, T> Collection = new ConcurrentDictionary<int, T>();

        public int Count => Collection.Count;

        public void AddOrUpdate(T newEntity) => Collection.AddOrUpdate(newEntity.ID, newEntity, (k, old) => newEntity);
        public void AddOrUpdate(IEnumerable<T> newEntities)
        {
            foreach (T entity in newEntities)
                AddOrUpdate(entity);
        }

        public T Retrieve(int key) => Collection[key];
        public IEnumerable<T> Retrieve(IEnumerable<int> keys)
        {
            HashSet<int> keyHashes = new HashSet<int>(keys);
            return Collection.Where(k => keyHashes.Contains(k.Key)).Select(v => v.Value);
        }
        public IEnumerable<T> RetrieveAll()
        {
            foreach (var pair in Collection)
                yield return pair.Value;
        }

        public void Insert(T newEntity) => Collection.AddOrUpdate(newEntity.ID, newEntity, (e, v) => newEntity);

        public void Remove(int key) => Collection.TryRemove(key, out T removed);

        public void Update(int id, T modifiedEntity)
        {
            try
            {
                Collection.AddOrUpdate(id, (k) => throw new KeyNotFoundException(), (k, e) => modifiedEntity);
            }
            catch (KeyNotFoundException)
            {
                //Entity was removed in this one's lifetime
            }
        }
    }
}
