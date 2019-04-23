using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Curds.Domain.Security;

namespace Curds.Persistence.EFCore
{
    public abstract class SecureCurdsContext : CurdsContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ReAuth> ReAuthentications { get; set; }

        public SecureCurdsContext(EFProvider provider)
            : base(provider)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Session>()
                .HasKey(nameof(Session.Identifier));
            modelBuilder.Entity<Session>()
                .HasIndex(nameof(Session.Series)).IsUnique();
            modelBuilder.Entity<Session>()
                .HasIndex(nameof(Session.UserID));
            modelBuilder.Entity<Session>()
                .HasIndex(nameof(Session.Expiration));
            modelBuilder.Entity<ReAuth>()
                .HasKey(nameof(ReAuth.Series));
            modelBuilder.Entity<ReAuth>()
                .HasIndex(nameof(ReAuth.UserID));
        }
    }
}
