using Curds.Persistence.EFCore.Persistor;
using Microsoft.EntityFrameworkCore;

namespace Curds.Domain.Persistence.EFCore.Persistor
{
    using Security;

    public class MockReAuthPersistor : BaseReAuthPersistor<MockSecureContext>
    {
        public MockReAuthPersistor(MockProvider provider)
            : base(provider)
        { }

        public override DbSet<ReAuth> Set(MockSecureContext context) => context.ReAuthentications;
    }
}
