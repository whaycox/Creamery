using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.DeferredValues.Domain
{
    public enum LabelDeferredKey : uint
    {
        None = default,
        AddButton,
        CancelButton,
        DeleteButton,
        Satellite,
        Satellites,
        SatelliteName,
        SatelliteIP,
        SatelliteStatus,
        AddSatellite,
        Check,
        Checks,
        CheckName,
        RescheduleInterval,
    }
}
