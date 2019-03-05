using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Microsoft.EntityFrameworkCore;
using Curds.Persistence.EFCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class Satellite : BasicPersistor<Domain.Communication.Satellite>
    {
        public Satellite(EFProvider<GoudaContext> provider)
            : base(provider)
        { }

        public override DbSet<Domain.Communication.Satellite> Set(GoudaContext context) => context.Satellites;
    }
}
