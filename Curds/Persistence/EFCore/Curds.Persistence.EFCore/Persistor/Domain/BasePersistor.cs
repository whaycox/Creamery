using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistor.Domain
{
    public abstract class BasePersistor<T, U>
        where U : class
        where T : DbContext
    {
        private bool DatabaseVerified = false;

        protected T Context
        {
            get
            {
                T context = ContextInternal;
                if (!DatabaseVerified)
                {
                    context.Database.EnsureCreated();
                    DatabaseVerified = true;
                }
                return context;
            }
        }
        protected abstract T ContextInternal { get; }

        protected abstract DbSet<U> Set(T context);
    }
}
