using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.Domain
{
    using Gouda.Domain;

    public class GoudaContext : DbContext
    {
        public DbSet<Satellite> Satellites { get; set; }

        public GoudaContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(nameof(Gouda));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GoudaContext).Assembly);
        }
    }
}
