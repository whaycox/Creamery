namespace Curds.Persistence.EFCore.Persistor.Mock
{
    using EFCore.Mock;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Mock;

    public class BasePersistor : Domain.BasePersistor<Context, Persistence.Mock.BaseEntity>
    {
        public Context ExposedContext => Context;
        public DbSet<Persistence.Mock.BaseEntity> ExposedSet(Context context) => Set(context);

        protected override Context ContextInternal => new Context();

        protected override DbSet<Persistence.Mock.BaseEntity> Set(Context context) => context.Entities;
    }
}
