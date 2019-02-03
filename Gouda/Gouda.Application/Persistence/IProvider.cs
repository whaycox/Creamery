using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Gouda.Domain;

namespace Gouda.Application.Persistence
{
    public interface IProvider
    {
        void PopulateCache();

        Definition LookupDefinition(int id);
        Satellite LookupSatellite(int id);
    }
}
