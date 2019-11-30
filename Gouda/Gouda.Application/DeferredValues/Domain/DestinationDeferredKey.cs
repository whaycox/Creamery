using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.DeferredValues.Domain
{
    public enum DestinationDeferredKey : uint
    {
        None = default,
        ListSatellites,
        AddSatellite,
    }
}
