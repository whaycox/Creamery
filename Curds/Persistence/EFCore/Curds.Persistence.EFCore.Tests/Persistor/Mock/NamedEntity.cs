using System.Collections.Generic;

namespace Curds.Persistence.EFCore.Persistor.Mock
{
    using EFCore.Mock;
    using Microsoft.EntityFrameworkCore;
    using Persistence.EventArgs;

    public class NamedEntity : Implementation.Entity<Context, Persistence.Mock.NamedEntity>
    {
        public Context ExposedContext => Context;
        protected override Context ContextInternal => new Context();

        public NamedEntity()
        {
            EntityAdded += AddEntity;
            EntityRemoved += RemoveEntity;
            EntityUpdated += UpdateEntity;
        }

        protected override DbSet<Persistence.Mock.NamedEntity> Set(Context context) => context.NamedEntities;

        public List<EntityModified<Persistence.Mock.NamedEntity>> EntitiesAdded = new List<EntityModified<Persistence.Mock.NamedEntity>>();
        private void AddEntity(object sender, EntityModified<Persistence.Mock.NamedEntity> eventArgs) => EntitiesAdded.Add(eventArgs);

        public List<EntityModified<Persistence.Mock.NamedEntity>> EntitiesRemoved = new List<EntityModified<Persistence.Mock.NamedEntity>>();
        private void RemoveEntity(object sender, EntityModified<Persistence.Mock.NamedEntity> eventArgs) => EntitiesRemoved.Add(eventArgs);

        public List<EntityModified<Persistence.Mock.NamedEntity>> EntitiesUpdated = new List<EntityModified<Persistence.Mock.NamedEntity>>();
        private void UpdateEntity(object sender, EntityModified<Persistence.Mock.NamedEntity> eventArgs) => EntitiesUpdated.Add(eventArgs);
    }
}
