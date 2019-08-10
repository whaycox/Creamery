using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistors.Mock
{
    using EFCore.Mock;
    using Persistence.Mock;

    public class IEntityPersistor : Implementation.EntityPersistor<Context, IEntity>
    {
        public Context ExposedContext => Context;
        protected override Context ContextInternal => new Context();

        protected override DbSet<IEntity> Set(Context context) => context.Entities;
    }
}
