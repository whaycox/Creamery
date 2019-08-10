using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Mock
{
    using Persistence.Mock;

    public class Context : DbContext
    {
        public DbSet<IEntity> Entities { get; set; }
        public DbSet<INamedEntity> NamedEntities { get; set; }
        public DbSet<INameValueEntity> NameValueEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(nameof(Context));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IEntity>().HasData(IEntity.Samples);
            modelBuilder.Entity<INamedEntity>().HasKey(nameof(INamedEntity.Name));
            modelBuilder.Entity<INamedEntity>().HasData(INamedEntity.Samples);
            modelBuilder.Entity<INameValueEntity>().HasData(INameValueEntity.Samples);
        }
    }
}
