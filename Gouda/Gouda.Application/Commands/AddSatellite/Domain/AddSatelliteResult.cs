using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Commands.AddSatellite.Domain
{
    using ViewModels.Satellite.Domain;

    public class AddSatelliteResult
    {
        public SatelliteSummaryViewModel NewSatellite { get; set; }
    }
}
