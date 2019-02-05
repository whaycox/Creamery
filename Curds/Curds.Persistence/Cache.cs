using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using Curds.Domain.Persistence;
using System.Linq;

namespace Curds.Persistence
{
    public class Cache<T> where T : Entity
    {
        private ConcurrentDictionary<int, T> Collection = new ConcurrentDictionary<int, T>();

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
    }
}
