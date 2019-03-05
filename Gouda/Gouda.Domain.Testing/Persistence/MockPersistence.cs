using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.Persistence;
using Gouda.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Curds;
using System.Diagnostics;

namespace Gouda.Domain.Persistence
{
    using Check;
    using Communication;
    using Security;

    public class MockPersistence : GoudaProvider
    {
        public MockPersistence(ICron cronProvider)
            : base(cronProvider)
        { }

        public void Reset()
        {
            using (GoudaContext context = Context)
            {
                Debug.WriteLine("Resetting context");
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        public void EmptyUsers()
        {
            foreach (User user in Users.FetchAll().AwaitResult())
                Users.Delete(user.ID);
        }

        public override void ConfigureContext(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase(nameof(MockPersistence));

        public override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Definition>().HasData(Seeds.Definition.Data);
            modelBuilder.Entity<DefinitionArgument>().HasData(Seeds.DefinitionArgument.Data);
            modelBuilder.Entity<DefinitionRegistration>().HasData(Seeds.DefinitionRegistration.Data);
            modelBuilder.Entity<Satellite>().HasData(Seeds.Satellite.Data);
            modelBuilder.Entity<User>().HasData(Seeds.User.Data);
            modelBuilder.Entity<Contact>().HasData(Seeds.Contact.Data);
            modelBuilder.Entity<ContactArgument>().HasData(Seeds.ContactArgument.Data);
            modelBuilder.Entity<ContactRegistration>().HasData(Seeds.ContactRegistration.Data);
        }
    }
}
