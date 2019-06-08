using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistor.Mock
{
    using EFCore.Mock;
    using Persistence.EventArgs;

    public class Entity : Implementation.Entity<Context, Persistence.Mock.Entity>
    {
        public Context ExposedContext => Context;
        protected override Context ContextInternal => new Context();

        public Entity()
        {
            EntityAdded += AddEntity;
            EntityRemoved += RemoveEntity;
            EntityUpdated += UpdateEntity;
        }

        protected override DbSet<Persistence.Mock.Entity> Set(Context context) => context.Entities;

        public List<EntityModified<Persistence.Mock.Entity>> EntitiesAdded = new List<EntityModified<Persistence.Mock.Entity>>();
        private void AddEntity(object sender, EntityModified<Persistence.Mock.Entity> eventArgs) => EntitiesAdded.Add(eventArgs);

        public List<EntityModified<Persistence.Mock.Entity>> EntitiesRemoved = new List<EntityModified<Persistence.Mock.Entity>>();
        private void RemoveEntity(object sender, EntityModified<Persistence.Mock.Entity> eventArgs) => EntitiesRemoved.Add(eventArgs);

        public List<EntityModified<Persistence.Mock.Entity>> EntitiesUpdated = new List<EntityModified<Persistence.Mock.Entity>>();
        private void UpdateEntity(object sender, EntityModified<Persistence.Mock.Entity> eventArgs) => EntitiesUpdated.Add(eventArgs);
    }
}
