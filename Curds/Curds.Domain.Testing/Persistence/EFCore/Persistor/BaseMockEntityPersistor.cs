using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore.Persistor;

namespace Curds.Domain.Persistence.EFCore.Persistor
{
    using EventArgs;

    public abstract class BaseMockEntityPersistor<T> : BaseEntityPersistor<MockSecureContext, T> where T : Entity
    {
        public List<EntityModifiedArgs<T>> AddedEvents = new List<EntityModifiedArgs<T>>();
        public List<EntityModifiedArgs<T>> UpdatedEvents = new List<EntityModifiedArgs<T>>();
        public List<EntityModifiedArgs<T>> RemovedEvents = new List<EntityModifiedArgs<T>>();

        public BaseMockEntityPersistor(MockProvider provider)
            : base(provider)
        {
            EntityAdded += (s, e) => AddedEvents.Add(e);
            EntityUpdated += (s, e) => UpdatedEvents.Add(e);
            EntityRemoved += (s, e) => RemovedEvents.Add(e);
        }
    }
}
