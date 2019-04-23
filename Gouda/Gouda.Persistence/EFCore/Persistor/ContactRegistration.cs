using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistor
{
    public class ContactRegistration : CronPersistor<Domain.Communication.ContactRegistration>
    {
        public ContactRegistration(GoudaProvider provider)
            : base(provider)
        { }

        protected override int OwningID(Domain.Communication.ContactRegistration registration) => registration.UserID;
        protected override int RegisteredID(Domain.Communication.ContactRegistration registration) => registration.ContactID;

        public override DbSet<Domain.Communication.ContactRegistration> Set(GoudaContext context) => context.ContactRegistrations;
    }
}
