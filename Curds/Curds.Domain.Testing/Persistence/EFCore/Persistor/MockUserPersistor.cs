using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore.Persistor;
using Curds.Domain.Security;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Curds.Domain.Persistence.EFCore.Persistor
{
    public class MockUserPersistor : BaseUserPersistor<MockSecureContext, User>
    {
        public MockUserPersistor(MockProvider provider)
            : base(provider)
        { }

        public void EmptyUsers()
        {
            using (MockSecureContext context = Provider.Context)
            {
                context.Users.RemoveRange(context.Users.ToList());
                context.SaveChanges();
            }
        }

        public override DbSet<User> Set(MockSecureContext context) => context.Users;
    }
}
