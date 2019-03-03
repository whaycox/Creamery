using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class User : EFPersistor<Domain.Security.User>
    {
        public User(EFProvider provider)
            : base(provider)
        { }

        internal override DbSet<Domain.Security.User> Set(GoudaContext context) => context.Users;
    }
}
