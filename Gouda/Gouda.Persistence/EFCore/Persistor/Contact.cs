using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Microsoft.EntityFrameworkCore;
using Curds.Persistence.EFCore;

namespace Gouda.Persistence.EFCore.Persistor
{
    public class Contact : BasicPersistor<Domain.Communication.Contact>
    {
        public Contact(EFProvider<GoudaContext> provider)
            : base(provider)
        { }

        public override DbSet<Domain.Communication.Contact> Set(GoudaContext context) => context.Contacts;
    }
}
