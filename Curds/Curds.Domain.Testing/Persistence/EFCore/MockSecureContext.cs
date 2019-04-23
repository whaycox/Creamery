using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Curds.Domain.Security;

namespace Curds.Domain.Persistence.EFCore
{
    public class MockSecureContext : SecureCurdsContext
    {
        public DbSet<MockEntity> Entities { get; set; }
        public DbSet<MockNamedEntity> NamedEntities { get; set; }
        public DbSet<MockNameValueEntity> NameValueEntities { get; set; }

        public MockSecureContext(MockProvider provider)
            : base(provider)
        { }
    }
}
