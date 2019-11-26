using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;
    using Enumerations;

    public class SatelliteSummaryViewModel : ISatelliteViewModel
    {
        public string ViewName => nameof(SatelliteSummaryViewModel);

        public int ID { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public SatelliteStatus Status { get; set; }
    }
}
