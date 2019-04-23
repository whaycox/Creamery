using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;
using Curds.Persistence.EFCore;
using Curds.Domain.Security;

namespace Gouda.Persistence.EFCore
{
    public class GoudaContext : SecureCurdsContext
    {
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<DefinitionArgument> DefinitionArguments { get; set; }
        public DbSet<DefinitionRegistration> DefinitionRegistrations { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactArgument> ContactArguments { get; set; }
        public DbSet<ContactRegistration> ContactRegistrations { get; set; }
        public DbSet<Satellite> Satellites { get; set; }

        public GoudaContext(EFProvider provider)
            : base(provider)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Satellite>()
                .Property(e => e.Endpoint)
                .HasConversion(
                    e => TypeConverters.Serialize(e),
                    e => TypeConverters.Deserialize(e));
            modelBuilder.Entity<User>()
                .HasAlternateKey(nameof(User.Email));
            modelBuilder.Entity<ReAuth>()
                .HasKey(nameof(ReAuth.UserID), nameof(ReAuth.Series), nameof(ReAuth.Token));
        }
    }
}
