using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Implementation
{
    using EFCore.Domain;
    using Security.Domain;

    public abstract class User<T> : BaseEntity<T, User>
        where T : SecureContext
    {
        protected override DbSet<User> Set(T context) => context.Users;

        public Task<User> FindByEmail(string email)
        {
            using (T context = Context)
                return FindByEmail(context, email);
        }
        public Task<User> FindByEmail(T context, string email) =>
            context.Users
                .Where(u => u.Email == email)
                .SingleAsync();
    }
}
