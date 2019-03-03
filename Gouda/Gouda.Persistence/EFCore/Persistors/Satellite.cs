using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.EFCore.Persistors
{
    public class Satellite : EFPersistor<Domain.Communication.Satellite>
    {
        public Satellite(EFProvider provider)
            : base(provider)
        { }

        internal override DbSet<Domain.Communication.Satellite> Set(GoudaContext context) => context.Satellites;
    }
}
