using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Mock
{
    using Persistence.Mock;

    public class Context : DbContext
    {
        public DbSet<BaseEntity> BaseEntities { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<NamedEntity> NamedEntities { get; set; }
        public DbSet<NameValueEntity> NameValueEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(nameof(Context));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaseEntity>().HasKey(nameof(BaseEntity.MyValue));
            modelBuilder.Entity<BaseEntity>().HasData(BaseEntity.Samples);
            modelBuilder.Entity<Entity>().HasData(Entity.Samples);
            modelBuilder.Entity<NamedEntity>().HasData(NamedEntity.Samples);
            modelBuilder.Entity<NameValueEntity>().HasData(NameValueEntity.Samples);
        }
    }
}
