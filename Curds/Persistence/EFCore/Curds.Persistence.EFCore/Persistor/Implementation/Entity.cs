using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Implementation
{
    using Persistence.Domain;
    using Persistence.EventArgs;

    public abstract class Entity<T, U> : BaseEntity<T, U>
        where T : DbContext
        where U : Entity
    {
        protected event EventHandler<EntityModified<U>> EntityRemoved;
        protected event EventHandler<EntityModified<U>> EntityUpdated;

        public async override Task<U> Insert(T context, U newEntity)
        {
            if (newEntity.ID != default(int))
                throw new ArgumentException("Cannot supply an entity with an existing ID");
            return await base.Insert(context, newEntity);
        }

        public async Task Delete(int id)
        {
            using (T context = Context)
            {
                await Delete(context, id);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(T context, int id)
        {
            U toDelete = await Lookup(context, id);
            context.Remove(toDelete);
            OnEntityRemoved(toDelete);
        }
        private void OnEntityRemoved(U entity) => EntityRemoved?.Invoke(this, new EntityModified<U>(entity));

        public async Task<List<U>> LookupMany(IEnumerable<int> ids)
        {
            using (T context = Context)
            {
                List<U> toReturn = new List<U>();
                foreach (int id in ids)
                    toReturn.Add(await Lookup(context, id));
                return toReturn;
            }
        }
        public async Task<U> Lookup(int id)
        {
            using (T context = Context)
                return await Lookup(context, id);
        }
        public async Task<U> Lookup(T context, int id)
        {
            U entity = await Set(context).FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No {typeof(U).Name} was found with id {id}");
            return entity;
        }

        public async Task Update(int id, Func<U, U> updateDelegate)
        {
            using (T context = Context)
            {
                await Update(context, id, updateDelegate);
                await context.SaveChangesAsync();
            }
        }
        public async Task Update(T context, int id, Func<U, U> updateDelegate)
        {
            U entity = await Lookup(context, id);
            entity = updateDelegate(entity);
            OnEntityModified(entity);
        }
        private void OnEntityModified(U entity) => EntityUpdated?.Invoke(this, new EntityModified<U>(entity));
    }
}
