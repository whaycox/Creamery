namespace Curds.Persistence.EFCore.Persistors.Mock
{
    using EFCore.Mock;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Mock;

    public class INamedEntityPersistor : Implementation.BasePersistor<Context, INamedEntity>
    {
        public Context ExposedContext => Context;
        protected override Context ContextInternal => new Context();

        protected override DbSet<INamedEntity> Set(Context context) => context.NamedEntities;
    }
}
