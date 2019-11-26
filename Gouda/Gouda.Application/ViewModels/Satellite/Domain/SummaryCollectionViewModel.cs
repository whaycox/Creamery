using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;

    public class SummaryCollectionViewModel : ISatelliteViewModel
    {
        private const string NameLabel = "Name";
        private const string IPLabel = "IP Address";
        private const string StatusLabel = "Status";

        public string ViewName => nameof(SummaryCollectionViewModel);

        public string NameHeaderLabel { get; set; } = NameLabel;
        public string IPHeaderLabel { get; set; } = IPLabel;
        public string StatusHeaderLabel { get; set; } = StatusLabel;
        public List<SatelliteSummaryViewModel> Satellites { get; set; } = new List<SatelliteSummaryViewModel>();
    }
}
