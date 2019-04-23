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
        private bool DatabaseVerified = false;

        public T Context
        {
            get
            {
                T context = ContextInternal;
                if (!DatabaseVerified)
                {
                    context.Database.EnsureCreated();
                    DatabaseVerified = true;
                }
                return context;
            }
        }
        protected abstract T ContextInternal { get; }
    }
}
