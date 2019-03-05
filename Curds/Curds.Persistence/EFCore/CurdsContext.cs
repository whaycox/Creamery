using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore
{
    public abstract class CurdsContext : DbContext 
    {
        protected EFProvider Provider { get; }

        public CurdsContext(EFProvider provider)
        {
            Provider = provider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Provider.SeedData(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => Provider.ConfigureContext(optionsBuilder);
    }
}
