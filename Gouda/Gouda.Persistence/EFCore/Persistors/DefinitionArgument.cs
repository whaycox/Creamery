using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class DefinitionArgument : EFPersistor<Domain.Check.DefinitionArgument>
    {
        public DefinitionArgument(EFProvider provider)
            : base(provider)
        { }

        internal override DbSet<Domain.Check.DefinitionArgument> Set(GoudaContext context) => context.DefinitionArguments;
    }
}
