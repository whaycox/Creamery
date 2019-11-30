using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;
    using DeferredValues.Domain;

    public class SummaryCollectionViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(SummaryCollectionViewModel);

        public LabelDeferredKey NameHeaderLabel { get; set; } = LabelDeferredKey.SatelliteName;
        public LabelDeferredKey IPHeaderLabel { get; set; } = LabelDeferredKey.SatelliteIP;
        public LabelDeferredKey StatusHeaderLabel { get; set; } = LabelDeferredKey.SatelliteStatus;
        public List<SatelliteSummaryViewModel> Satellites { get; set; } = new List<SatelliteSummaryViewModel>();
    }
}
