using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Template
{
    public abstract class DbContext<T, U> : Test<T>
        where U : DbContext
    {
        protected abstract U BuildContext();

        [TestInitialize]
        public void DeleteDatabase()
        {
            using (U context = BuildContext())
                context.Database.EnsureDeleted();
        }
    }
}
