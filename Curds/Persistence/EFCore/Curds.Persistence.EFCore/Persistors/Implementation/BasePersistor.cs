using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistors.Implementation
{
    using Persistence.Persistors.Abstraction;

    public abstract class BasePersistor<T, U> : IPersistor<U>
        where T : DbContext
        where U : class
    {
        private bool DatabaseVerified = false;

        protected T Context
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

        protected abstract DbSet<U> Set(T context);

        public async Task<int> Count()
        {
            using (T context = Context)
                return await Count(context);
        }
        public Task<int> Count(T context) => Set(context).CountAsync();

        public async Task<List<U>> FetchAll()
        {
            using (T context = Context)
                return await FetchAll(context);
        }
        public Task<List<U>> FetchAll(T context) => Set(context).AsNoTracking().ToListAsync();

        public async Task<U> Insert(U newEntity)
        {
            using (T context = Context)
            {
                U inserted = await Insert(context, newEntity);
                await context.SaveChangesAsync();
                return inserted;
            }
        }
        public virtual async Task<U> Insert(T context, U newEntity)
        {
            var entry = await context.AddAsync(newEntity);
            return entry.Entity;
        }
    }
}
