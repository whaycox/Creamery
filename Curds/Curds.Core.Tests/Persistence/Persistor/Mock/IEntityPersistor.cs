using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Mock
{
    using Domain;

    public class IEntityPersistor : ProtoMock<Entity>, Abstraction.IEntityPersistor<Entity>
    {
        protected override List<Entity> Samples => new List<Entity>
        {
            new Persistence.Mock.Entity(1),
            new Persistence.Mock.Entity(2),
            new Persistence.Mock.Entity(3),
        };

        public Task<int> Count => Task.Run(() => Samples.Count);

        public List<int> DeletedIDs = new List<int>();
        public Task Delete(int id) => Task.Run(() => DeletedIDs.Add(id));

        public Task<List<Entity>> FetchAll() => Task.Run(() => Samples);

        public List<Entity> InsertedEntities = new List<Entity>();
        public Task<Entity> Insert(Entity newEntity) => Task.Run(() => InsertInternal(newEntity));
        private Entity InsertInternal(Entity newEntity)
        {
            InsertedEntities.Add(newEntity);
            return newEntity;
        }

        public Task<Entity> Lookup(int id) => Task.Run(() => LookupInternal(id));
        private Entity LookupInternal(int id) => Samples.Where(s => s.ID == id).FirstOrDefault();

        public Task<List<Entity>> LookupMany(IEnumerable<int> ids) => Task.Run(() => LookupManyInternal(ids));
        private List<Entity> LookupManyInternal(IEnumerable<int> ids) => Samples.Where(s => ids.Contains(s.ID)).ToList();

        public List<int> UpdatedEntities = new List<int>();
        public Task Update(int id, Func<Entity, Entity> updateDelegate) => Task.Run(() => UpdatedEntities.Add(id));
    }
}
