using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Domain
{
    using Security.Domain;

    public class SecureContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ReAuth> ReAuthentications { get; set; }

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
