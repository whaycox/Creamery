using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Curds.Domain.Persistence.EFCore.Persistors
{
    public class MockNameValueEntityPersistor : EFPersistor<MockContext, MockNameValueEntity>
    {
        public MockNameValueEntityPersistor(MockProvider provider)
            : base(provider)
        { }

        public override DbSet<MockNameValueEntity> Set(MockContext context) => context.NameValueEntities;
    }
}
