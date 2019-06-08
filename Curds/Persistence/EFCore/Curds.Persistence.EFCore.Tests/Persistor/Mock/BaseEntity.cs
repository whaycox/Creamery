using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistor.Mock
{
    using Curds.Persistence.Mock;
    using EFCore.Mock;
    using Persistence.EventArgs;

    public class BaseEntity : Implementation.BaseEntity<Context, Persistence.Mock.BaseEntity>
    {
        public Context ExposedContext => Context;
        protected override Context ContextInternal => new Context();

        public BaseEntity()
        {
            EntityAdded += AddEntity;
        }

        protected override DbSet<Persistence.Mock.BaseEntity> Set(Context context) => context.BaseEntities;

        public List<EntityModified<Persistence.Mock.BaseEntity>> EntitiesAdded = new List<EntityModified<Persistence.Mock.BaseEntity>>();
        private void AddEntity(object sender, EntityModified<Persistence.Mock.BaseEntity> eventArgs) => EntitiesAdded.Add(eventArgs);
    }
}
