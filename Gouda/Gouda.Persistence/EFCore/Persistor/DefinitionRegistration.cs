using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistor
{
    public class DefinitionRegistration : CronPersistor<Domain.Check.DefinitionRegistration>
    {
        public DefinitionRegistration(GoudaProvider provider)
            : base(provider)
        { }

        protected override int OwningID(Domain.Check.DefinitionRegistration registration) => registration.DefinitionID;
        protected override int RegisteredID(Domain.Check.DefinitionRegistration registration) => registration.UserID;

        public override DbSet<Domain.Check.DefinitionRegistration> Set(GoudaContext context) => context.DefinitionRegistrations;
    }
}
