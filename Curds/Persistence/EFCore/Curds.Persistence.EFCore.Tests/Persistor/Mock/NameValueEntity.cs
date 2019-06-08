using System.Collections.Generic;

namespace Curds.Persistence.EFCore.Persistor.Mock
{
    using EFCore.Mock;
    using Microsoft.EntityFrameworkCore;
    using Persistence.EventArgs;

    public class NameValueEntity : Implementation.Entity<Context, Persistence.Mock.NameValueEntity>
    {
        public Context ExposedContext => Context;
        protected override Context ContextInternal => new Context();

        public NameValueEntity()
        {
            EntityAdded += AddEntity;
            EntityRemoved += RemoveEntity;
            EntityUpdated += UpdateEntity;
        }

        protected override DbSet<Persistence.Mock.NameValueEntity> Set(Context context) => context.NameValueEntities;

        public List<EntityModified<Persistence.Mock.NameValueEntity>> EntitiesAdded = new List<EntityModified<Persistence.Mock.NameValueEntity>>();
        private void AddEntity(object sender, EntityModified<Persistence.Mock.NameValueEntity> eventArgs) => EntitiesAdded.Add(eventArgs);

        public List<EntityModified<Persistence.Mock.NameValueEntity>> EntitiesRemoved = new List<EntityModified<Persistence.Mock.NameValueEntity>>();
        private void RemoveEntity(object sender, EntityModified<Persistence.Mock.NameValueEntity> eventArgs) => EntitiesRemoved.Add(eventArgs);

        public List<EntityModified<Persistence.Mock.NameValueEntity>> EntitiesUpdated = new List<EntityModified<Persistence.Mock.NameValueEntity>>();
        private void UpdateEntity(object sender, EntityModified<Persistence.Mock.NameValueEntity> eventArgs) => EntitiesUpdated.Add(eventArgs);
    }
}
