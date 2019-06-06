using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Implementation
{
    using Domain;
    using Persistence.Domain;
    using Persistence.EventArgs;
    using Persistence.Persistor.Abstraction;

    public abstract class BaseEntity<T, U> : BasePersistor<T, U>, IBaseEntity<U>
        where T : DbContext
        where U : BaseEntity
    {
        protected event EventHandler<EntityModified<U>> EntityAdded;

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
            OnEntityAdded(entry.Entity);
            return entry.Entity;
        }
        private void OnEntityAdded(U entity) => EntityAdded?.Invoke(this, new EntityModified<U>(entity));
    }
}
