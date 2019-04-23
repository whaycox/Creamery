using Curds.Application.Persistence.Persistor;
using Curds.Domain.Security;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BaseUserPersistor<T, U> : BaseEntityPersistor<T, U>, IUserPersistor<U>
        where T : SecureCurdsContext
        where U : User
    {
        public BaseUserPersistor(EFProvider<T> provider)
            : base(provider)
        { }

        public Task<User> FindByEmail(string email)
        {
            using (T context = Provider.Context)
                return FindByEmail(email, context);
        }
        private Task<User> FindByEmail(string email, T context) =>
            context.Users
                .Where(u => u.Email == email)
                .SingleAsync();
    }
}
