using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;


namespace Curds.Domain.Persistence.EFCore.Persistors
{
    using EventArgs;

    public class MockNameValueEntityPersistor : EFPersistor<MockContext, MockNameValueEntity>
    {
        public List<EntityModifiedArgs<MockNameValueEntity>> AddedEvents = new List<EntityModifiedArgs<MockNameValueEntity>>();
        public List<EntityModifiedArgs<MockNameValueEntity>> UpdatedEvents = new List<EntityModifiedArgs<MockNameValueEntity>>();
        public List<EntityModifiedArgs<MockNameValueEntity>> RemovedEvents = new List<EntityModifiedArgs<MockNameValueEntity>>();

        public MockNameValueEntityPersistor(MockProvider provider)
            : base(provider)
        {
            EntityAdded += (s, e) => AddedEvents.Add(e);
            EntityUpdated += (s, e) => UpdatedEvents.Add(e);
            EntityRemoved += (s, e) => RemovedEvents.Add(e);
        }

        public override DbSet<MockNameValueEntity> Set(MockContext context) => context.NameValueEntities;
    }
}
