using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Mock
{
    using Persistence.Mock;

    public class Context : DbContext
    {
        public DbSet<BaseEntity> Entities { get; set; }

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
        }
    }
}
