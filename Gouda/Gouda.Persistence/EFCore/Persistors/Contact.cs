using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class Contact : EFPersistor<Domain.Communication.Contact>
    {
        public Contact(EFProvider provider)
            : base(provider)
        { }

        internal override DbSet<Domain.Communication.Contact> Set(GoudaContext context) => context.Contacts;
    }
}
