using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Enumerations
{
    public enum SatelliteStatus
    {
        Unknown = default,
        Failing,
        Warning,
        Worried,
        Good,
    }
}
