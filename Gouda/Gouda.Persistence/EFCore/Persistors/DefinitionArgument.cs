using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Curds.Persistence.EFCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class DefinitionArgument : BasicPersistor<Domain.Check.DefinitionArgument>
    {
        public DefinitionArgument(EFProvider<GoudaContext> provider)
            : base(provider)
        { }

        public override DbSet<Domain.Check.DefinitionArgument> Set(GoudaContext context) => context.DefinitionArguments;
    }
}
