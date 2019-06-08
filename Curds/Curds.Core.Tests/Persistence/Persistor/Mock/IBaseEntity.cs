using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Mock
{
    using Domain;

    public class IBaseEntity : ProtoMock<BaseEntity>, Abstraction.IBaseEntity<BaseEntity>
    {
        protected override List<BaseEntity> Samples => new List<BaseEntity>
        {
            new Persistence.Mock.BaseEntity() { MyValue = 1 },
            new Persistence.Mock.BaseEntity() { MyValue = 2 },
            new Persistence.Mock.BaseEntity() { MyValue = 3 },
        };

        public Task<int> Count() => Task.Run(() => Samples.Count);

        public Task<List<BaseEntity>> FetchAll() => Task.Run(() => Samples);

        public List<BaseEntity> InsertedEntities = new List<BaseEntity>();
        public Task<BaseEntity> Insert(BaseEntity newEntity) => Task.Run(() => InsertInternal(newEntity));
        private BaseEntity InsertInternal(BaseEntity newEntity)
        {
            InsertedEntities.Add(newEntity);
            return newEntity;
        }
    }
}
