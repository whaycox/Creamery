using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore.Persistor;
using Microsoft.EntityFrameworkCore;

namespace Curds.Domain.Persistence.EFCore.Persistor
{
    public class MockNameValueEntityPersistor : BaseMockEntityPersistor<MockNameValueEntity>
    {
        public MockNameValueEntityPersistor(MockProvider provider)
            : base(provider)
        { }

        public override DbSet<MockNameValueEntity> Set(MockSecureContext context) => context.NameValueEntities;
    }
}
