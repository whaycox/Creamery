using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Curds.Domain.Persistence.EFCore
{
    public class MockContext : CurdsContext
    {
        public DbSet<MockEntity> Entities { get; set; }
        public DbSet<MockNamedEntity> NamedEntities { get; set; }
        public DbSet<MockNameValueEntity> NameValueEntities { get; set; }

        public MockContext(MockProvider provider)
            : base(provider)
        { }
    }
}
