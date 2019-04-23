using Curds.Application.Persistence.Persistor;
using Curds.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Gouda.Persistence.EFCore.Persistor
{
    public class User : BasicPersistor<Curds.Domain.Security.User>, IUserPersistor<Curds.Domain.Security.User>
    {
        public User(EFProvider<GoudaContext> provider)
            : base(provider)
        { }

        public Task<Curds.Domain.Security.User> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override DbSet<Curds.Domain.Security.User> Set(GoudaContext context) => context.Users;
    }
}
