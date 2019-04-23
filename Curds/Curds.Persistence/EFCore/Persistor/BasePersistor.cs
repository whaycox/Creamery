using Curds.Application.Persistence.Persistor;
using Curds.Domain.EventArgs;
using Curds.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BasePersistor<T, U> : IPersistor<U> 
        where T : CurdsContext 
        where U : BaseEntity
    {
        protected event EventHandler<EntityModifiedArgs<U>> EntityAdded;

        protected EFProvider<T> Provider { get; }

        public Task<int> Count
        {
            get
            {
                using (T context = Provider.Context)
                    return Set(context).CountAsync();
            }
        }

        public BasePersistor(EFProvider<T> provider)
        {
            Provider = provider;
        }

        public abstract DbSet<U> Set(T context);

        public async Task<List<U>> FetchAll()
        {
            using (T context = Provider.Context)
            {
                DbSet<U> set = Set(context);
                return await set.AsNoTracking().ToListAsync();
            }
        }

        public async Task<U> Insert(U newEntity)
        {
            using (T context = Provider.Context)
            {
                U inserted = await Insert(newEntity, context);
                await context.SaveChangesAsync();
                return inserted;
            }
        }
        protected virtual async Task<U> Insert(U newEntity, T context)
        {
            var entry = await context.AddAsync(newEntity);
            OnEntityAdded(entry.Entity);
            return entry.Entity;
        }
        private void OnEntityAdded(U entity) => EntityAdded?.Invoke(this, new EntityModifiedArgs<U>(entity));
    }
}
