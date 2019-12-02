using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Queries.ListSatellites.Domain
{
    using ViewModels.Satellite.Domain;
    using DeferredValues.Domain;

    public class ListSatellitesResult
    {
        public LabelDeferredKey NameHeaderLabel { get; set; } = LabelDeferredKey.SatelliteName;
        public LabelDeferredKey IPHeaderLabel { get; set; } = LabelDeferredKey.SatelliteIP;
        public LabelDeferredKey StatusHeaderLabel { get; set; } = LabelDeferredKey.SatelliteStatus;
        public List<SatelliteSummaryViewModel> Satellites { get; set; } = new List<SatelliteSummaryViewModel>();
    }
}
