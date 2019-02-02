using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Domain.Persistence
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
    }
}
