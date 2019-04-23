using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Microsoft.EntityFrameworkCore;
using Curds.Persistence.EFCore;

namespace Gouda.Persistence.EFCore.Persistor
{
    public class Definition : BasicPersistor<Domain.Check.Definition>
    {
        public Definition(EFProvider<GoudaContext> provider)
            : base(provider)
        { }

        public override DbSet<Domain.Check.Definition> Set(GoudaContext context) => context.Definitions;
    }
}
