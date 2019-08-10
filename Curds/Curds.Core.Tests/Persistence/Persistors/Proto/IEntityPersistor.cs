using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistors.Proto
{
    using Persistence.Abstraction;

    public abstract class IEntityPersistor<T> : Abstraction.IEntityPersistor<T> where T : IEntity
    {
        protected abstract List<T> Samples { get; }

        public Task<int> Count() => Task.Run(() => Samples.Count);

        public List<int> DeletedIDs = new List<int>();
        public Task Delete(int id) => Task.Run(() => DeletedIDs.Add(id));

        public Task<List<T>> FetchAll() => Task.FromResult(Samples);

        public List<T> InsertedEntities = new List<T>();
        public Task<T> Insert(T newEntity) => Task.FromResult(InsertInternal(newEntity));
        private T InsertInternal(T newEntity)
        {
            InsertedEntities.Add(newEntity);
            return newEntity;
        }

        public Task<T> Lookup(int id) => Task.FromResult(LookupInternal(id));
        private T LookupInternal(int id) => Samples.Where(s => s.ID == id).FirstOrDefault();

        public Task<List<T>> LookupMany(IEnumerable<int> ids) => Task.FromResult(LookupManyInternal(ids));
        private List<T> LookupManyInternal(IEnumerable<int> ids) => Samples.Where(s => ids.Contains(s.ID)).ToList();

        public List<int> UpdatedEntities = new List<int>();
        public Task Update(int id, Func<T, T> updateDelegate) => Task.Run(() => UpdatedEntities.Add(id));
    }
}
