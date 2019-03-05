using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore
{
    public abstract class EFProvider
    {
        public virtual void SeedData(ModelBuilder modelBuilder) { }

        public abstract void ConfigureContext(DbContextOptionsBuilder optionsBuilder);
    }

    public abstract class EFProvider<T> : EFProvider where T : CurdsContext
    {
        public abstract T Context { get; }
    }
}
