using Curds.Application.Persistence.Persistor;
using Curds.Domain.EventArgs;
using Curds.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BaseEntityPersistor<T, U> : BasePersistor<T, U>, IEntityPersistor<U>
        where T : CurdsContext
        where U : Entity
    {
        protected event EventHandler<EntityModifiedArgs<U>> EntityRemoved;
        protected event EventHandler<EntityModifiedArgs<U>> EntityUpdated;

        public BaseEntityPersistor(EFProvider<T> provider)
            : base(provider)
        { }

        protected async override Task<U> Insert(U newEntity, T context)
        {
            if (newEntity.ID != default(int))
                throw new InvalidOperationException("Cannot supply an entity with an existing ID");
            return await base.Insert(newEntity, context);
        }

        public async Task Delete(int id)
        {
            using (T context = Provider.Context)
            {
                await Delete(id, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Delete(int id, T context)
        {
            U toDelete = await Lookup(id, context);
            context.Remove(toDelete);
            OnEntityRemoved(toDelete);
        }
        private void OnEntityRemoved(U entity) => EntityRemoved?.Invoke(this, new EntityModifiedArgs<U>(entity));

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
        private async Task<U> Lookup(int id, T context)
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
                await Update(id, updateDelegate, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Update(int id, Func<U, U> updateDelegate, T context)
        {
            U entity = await Lookup(id, context);
            entity = updateDelegate(entity);
            OnEntityModified(entity);
        }
        private void OnEntityModified(U entity) => EntityUpdated?.Invoke(this, new EntityModifiedArgs<U>(entity));
    }
}
