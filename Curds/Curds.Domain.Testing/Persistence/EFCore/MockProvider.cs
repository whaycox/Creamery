using Curds.Application.Persistence;
using Curds.Application.Persistence.Persistor;
using Curds.Domain.Security;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Curds.Domain.Persistence.EFCore
{
    using Persistor;

    public class MockProvider : EFProvider<MockSecureContext>, ISecurityPersistence
    {
        public MockUserPersistor MockUsers = null;
        public IUserPersistor<User> Users => MockUsers;
        public ISessionPersistor<Session> Sessions { get; }
        public IReAuthPersistor<ReAuth> ReAuthentications { get; }

        protected override MockSecureContext ContextInternal => new MockSecureContext(this);

        public MockProvider()
        {
            MockUsers = new MockUserPersistor(this);
            Sessions = new MockSessionPersistor(this);
            ReAuthentications = new MockReAuthPersistor(this);
        }

        public void Reset()
        {
            using (MockSecureContext context = ContextInternal)
            {
                Debug.WriteLine("Resetting the context");
                context.Database.EnsureDeleted();
            }
        }

        public override void ConfigureContext(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase(nameof(MockProvider));

        public override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(MockUser.Samples);
        }
    }
}
