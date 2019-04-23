using Curds.Domain.Security;
using Curds.Persistence.EFCore.Persistor;
using Microsoft.EntityFrameworkCore;

namespace Curds.Domain.Persistence.EFCore.Persistor
{
    public class MockSessionPersistor : BaseSessionPersistor<MockSecureContext>
    {
        public MockSessionPersistor(MockProvider provider)
            : base(provider)
        { }

        public override DbSet<Session> Set(MockSecureContext context) => context.Sessions;
    }
}
