using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.Persistence;
using Gouda.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Curds;

namespace Gouda.Domain.Persistence
{
    using Check;
    using Communication;
    using Security;

    public class MockPersistence : EFProvider
    {
        public MockPersistence(ICron cronProvider)
            : base(cronProvider)
        { }

        public void Reset()
        {
            using (GoudaContext context = Context)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        public void EmptyUsers()
        {
            foreach (User user in Users.FetchAll().AwaitResult())
                Users.Delete(user.ID);
        }
        
        internal override void ConfigureContext(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase(nameof(MockPersistence));

        internal override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Definition>().HasData(Seeds.Definition.Data);
            modelBuilder.Entity<DefinitionArgument>().HasData(Seeds.DefinitionArgument.Data);
            modelBuilder.Entity<DefinitionRegistration>().HasData(Seeds.DefinitionRegistration.Data);
            modelBuilder.Entity<Satellite>().HasData(Seeds.Satellite.Data);
            modelBuilder.Entity<User>().HasData(Seeds.User.Data);
            modelBuilder.Entity<Contact>().HasData(Seeds.Contact.Data);
            modelBuilder.Entity<ContactArgument>().HasData(Seeds.ContactArgument.Data);
        }
    }
}
