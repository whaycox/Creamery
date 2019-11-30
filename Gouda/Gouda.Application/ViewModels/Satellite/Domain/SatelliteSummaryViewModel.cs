using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;
    using Enumerations;
    using Common.Domain;
    using DeferredValues.Domain;

    public class SatelliteSummaryViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(SatelliteSummaryViewModel);

        public int ID { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public SatelliteStatusViewModel StatusViewModel { get; set; } = new SatelliteStatusViewModel();
    }
}
