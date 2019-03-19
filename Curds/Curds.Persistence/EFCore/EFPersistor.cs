using Curds.Application.Persistence;
using Curds.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Curds.Domain.EventArgs;

namespace Curds.Persistence.EFCore
{
    public abstract class EFPersistor<T, U> : IPersistor<U> where T : CurdsContext where U : Entity
    {
        protected event EventHandler<EntityModifiedArgs<U>> EntityAdded;
        protected event EventHandler<EntityModifiedArgs<U>> EntityUpdated;
        protected event EventHandler<EntityModifiedArgs<U>> EntityRemoved;

        private EFProvider<T> Provider { get; }
        
        public Task<int> Count
        {
            get
            {
                using (T context = Provider.Context)
                    return Set(context).CountAsync();
            }
        }

        public EFPersistor(EFProvider<T> provider)
        {
            Provider = provider;
        }
        
        public abstract DbSet<U> Set(T context);

        public async Task Delete(int id)
        {
            using (T context = Provider.Context)
            {
                await Delete(id, context);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id, T context)
        {
            U toDelete = await Lookup(id, context);
            context.Remove(toDelete);
            OnEntityRemoved(toDelete);
        }
        private void OnEntityRemoved(U entity) => EntityRemoved?.Invoke(this, new EntityModifiedArgs<U>(entity));

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
        public async Task<U> Insert(U newEntity, T context)
        {
            if (newEntity.ID != default(int))
                throw new InvalidOperationException($"Cannot insert a {typeof(U).FullName} that has an ID");
            var entry =  await context.AddAsync(newEntity);
            OnEntityAdded(entry.Entity);
            return entry.Entity;
        }
        private void OnEntityAdded(U entity) => EntityAdded?.Invoke(this, new EntityModifiedArgs<U>(entity));

        public async Task<List<U>> LookupMany(IEnumerable<int> ids)
        {
            using (T context = Provider.Context)
            {
                List<U> toReturn = new List<U>();
                foreach (int id in ids)
                    toReturn.Add(await Lookup(id, context));
                return toReturn;
            }
        }
        public async Task<U> Lookup(int id)
        {
            using (T context = Provider.Context)
                return await Lookup(id, context);
        }
        public async Task<U> Lookup(int id, T context)
        {
            DbSet<U> set = Set(context);
            U entity = await set.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No {typeof(T).Name} was found with id {id}");
            return entity;
        }

        public async Task Update(int id, Func<U, U> updateDelegate)
        {
            using (T context = Provider.Context)
            {
                U entity = await Lookup(id, context);
                entity = updateDelegate(entity);
                OnEntityModified(entity);
                await context.SaveChangesAsync();
            }
        }
        private void OnEntityModified(U entity) => EntityUpdated?.Invoke(this, new EntityModifiedArgs<U>(entity));
    }
}
