using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Template
{
    using Mock;

    public abstract class MockContextTemplate<T> : Test<T>
    {
        [TestInitialize]
        public void DeleteDatabase()
        {
            using (Context context = new Context())
                context.Database.EnsureDeleted();
        }
    }
}
