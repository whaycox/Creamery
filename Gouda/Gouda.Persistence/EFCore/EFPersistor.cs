using Curds.Application.Persistence;
using Curds.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gouda.Persistence.EFCore
{
    public abstract class EFPersistor<T> : IPersistor<T> where T : Entity
    {
        private EFProvider Provider { get; }
        
        public Task<int> Count
        {
            get
            {
                using (GoudaContext context = Provider.Context)
                    return Set(context).CountAsync();
            }
        }

        public EFPersistor(EFProvider provider)
        {
            Provider = provider;
        }
        
        internal abstract DbSet<T> Set(GoudaContext context);

        public async Task Delete(int id)
        {
            using (GoudaContext context = Provider.Context)
            {
                await Delete(id, context);
                await context.SaveChangesAsync();
            }
        }
        internal async Task Delete(int id, GoudaContext context)
        {
            T toDelete = await Lookup(id, context);
            context.Remove(toDelete);
        }

        public async Task<List<T>> FetchAll()
        {
            using (GoudaContext context = Provider.Context)
            {
                DbSet<T> set = Set(context);
                return await set.AsNoTracking().ToListAsync();
            }
        }

        public async Task<T> Insert(T newEntity)
        {
            using (GoudaContext context = Provider.Context)
            {
                T inserted = await Insert(newEntity, context);
                await context.SaveChangesAsync();
                return inserted;
            }
        }
        internal async Task<T> Insert(T newEntity, GoudaContext context)
        {
            var entry =  await context.AddAsync(newEntity);
            return entry.Entity;
        }

        public Task<T> Lookup(int id)
        {
            using (GoudaContext context = Provider.Context)
                return Lookup(id, context);
        }
        internal async Task<T> Lookup(int id, GoudaContext context)
        {
            DbSet<T> set = Set(context);
            T entity = await set.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No {typeof(T).Name} was found with id {id}");
            return entity;
        }

        public IEnumerable<T> Lookup(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, Func<T, T> updateDelegate)
        {
            using (GoudaContext context = Provider.Context)
            {
                T entity = await Lookup(id, context);
                entity = updateDelegate(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
