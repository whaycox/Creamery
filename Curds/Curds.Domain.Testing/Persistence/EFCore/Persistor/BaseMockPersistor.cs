using Curds.Persistence.EFCore.Persistor;
using System.Collections.Generic;

namespace Curds.Domain.Persistence.EFCore.Persistor
{
    using EventArgs;

    public abstract class BaseMockPersistor<T> : BasePersistor<MockSecureContext, T> where T : BaseEntity
    {
        public List<EntityModifiedArgs<T>> AddedEvents = new List<EntityModifiedArgs<T>>();

        public BaseMockPersistor(MockProvider provider)
            : base(provider)
        {
            EntityAdded += (s, e) => AddedEvents.Add(e);
        }
    }
}
