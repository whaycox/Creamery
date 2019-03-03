using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class Definition : EFPersistor<Domain.Check.Definition>
    {
        public Definition(EFProvider provider)
            : base(provider)
        { }

        internal override DbSet<Domain.Check.Definition> Set(GoudaContext context) => context.Definitions;
    }
}
