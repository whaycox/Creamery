using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Curds.Domain.Persistence.EFCore
{
    public class MockProvider : EFProvider<MockContext>
    {
        public override MockContext Context => new MockContext(this);

        public void Reset()
        {
            using (MockContext context = Context)
            {
                Debug.WriteLine("Resetting the context");
                context.Database.EnsureDeleted();
            }
        }

        public override void ConfigureContext(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase(nameof(MockProvider));
    }
}
