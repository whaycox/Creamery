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
        
        public int Count => throw new NotImplementedException();

        public EFPersistor(EFProvider provider)
        {
            Provider = provider;
        }
        
        internal abstract DbSet<T> Set(GoudaContext context);

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> FetchAll()
        {
            using (GoudaContext context = Provider.Context)
            {
                DbSet<T> set = Set(context);
                return await set.ToListAsync();
            }
        }

        public T Insert(T newEntity)
        {
            throw new NotImplementedException();
        }
        internal async Task<T> Insert(T newEntity, GoudaContext context)
        {
            throw new NotImplementedException();
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
