using Curds;
using Curds.Application.Cron;
using Curds.Domain.EventArgs;
using Curds.Domain.Persistence;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Gouda.Persistence.EFCore.Persistor
{
    public abstract class CronPersistor<T> : BasicPersistor<T> where T : Entity, ICronEntity
    {
        private object Locker = new object();

        private ConcurrentDictionary<int, ConcurrentDictionary<int, CronEntity>> Entities = new ConcurrentDictionary<int, ConcurrentDictionary<int, CronEntity>>();

        private ICron Cron { get; }

        public CronPersistor(GoudaProvider provider)
            : base(provider)
        {
            Cron = provider.Cron;

            EntityAdded += AddEntity;
            EntityUpdated += UpdateEntity;
            EntityRemoved += RemoveEntity;
        }

        public void LoadEntities()
        {
            foreach (T entity in FetchAll().AwaitResult())
                AddEntity(entity);
        }

        private ConcurrentDictionary<int, CronEntity> InitialRegistration(T initialEntity)
        {
            ConcurrentDictionary<int, CronEntity> toReturn = new ConcurrentDictionary<int, CronEntity>();
            toReturn.AddConcurrently(initialEntity.ID, BuildEntity(initialEntity));
            return toReturn;
        }
        private CronEntity BuildEntity(T entity) => new CronEntity(RegisteredID(entity), Cron.Build(entity.CronString));

        private void AddEntity(object sender, EntityModifiedArgs<T> args) => AddEntity(args.Entity);
        private void AddEntity(T entity)
        {
            int owningID = OwningID(entity);
            if (!Entities.ContainsKey(owningID))
                Entities.AddConcurrently(owningID, InitialRegistration(entity));
            else
            {
                var registrations = Entities[owningID];
                registrations.AddConcurrently(entity.ID, BuildEntity(entity));
            }
        }

        private void UpdateEntity(object sender, EntityModifiedArgs<T> args)
        {
            T updatedEntity = args.Entity;
            int owningID = OwningID(updatedEntity);
            var registrations = Entities[owningID];
            CronEntity cronEntity = BuildEntity(updatedEntity);
            registrations.AddOrUpdate(updatedEntity.ID, cronEntity, (k, v) => cronEntity);
        }
        private void RemoveEntity(object sender, EntityModifiedArgs<T> args)
        {
            T removedEntity = args.Entity;
            int owningID = OwningID(removedEntity);
            var registrations = Entities[owningID];
            registrations.RemoveConcurrently(removedEntity.ID);
            if (registrations.Count == 0)
                Entities.RemoveConcurrently(owningID);
        }

        protected abstract int OwningID(T registration);
        protected abstract int RegisteredID(T registration);

        public List<int> FilterMany(List<int> owningIDs, DateTime filterTime) => owningIDs.SelectMany(i => Filter(i, filterTime)).ToList();
        public List<int> Filter(int owningID, DateTime filterTime)
        {
            if (!Entities.ContainsKey(owningID))
                return new List<int>();
            else
            {
                var registrations = Entities[owningID];
                return registrations.Where(e => e.Value.CronObject.Test(filterTime)).Select(e => e.Value.ID).ToList();
            }
        }

        private class CronEntity
        {
            public int ID { get; }
            public ICronObject CronObject { get; }

            public CronEntity(int registeredID, ICronObject cronObject)
            {
                ID = registeredID;
                CronObject = cronObject;
            }
        }
    }
}
