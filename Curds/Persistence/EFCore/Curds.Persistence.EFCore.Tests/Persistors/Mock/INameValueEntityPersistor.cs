using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistors.Mock
{
    using EFCore.Mock;
    using Persistence.Mock;

    public class INameValueEntityPersistor : Implementation.EntityPersistor<Context, INameValueEntity>
    {
        protected override Context ContextInternal => new Context();
        protected override DbSet<INameValueEntity> Set(Context context) => context.NameValueEntities;
    }
}
