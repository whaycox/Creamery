using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Enumerations;
    using Abstraction;

    public class SatelliteStatusViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(SatelliteStatusViewModel);

        public SatelliteStatus Status { get; set; }
    }
}
