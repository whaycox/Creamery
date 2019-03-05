using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Curds.Persistence.EFCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class User : BasicPersistor<Domain.Security.User>
    {
        public User(EFProvider<GoudaContext> provider)
            : base(provider)
        { }

        public override DbSet<Domain.Security.User> Set(GoudaContext context) => context.Users;
    }
}
