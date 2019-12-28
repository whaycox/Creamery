using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Commands.AddSatellite.Domain
{
    using ViewModels.Satellite.Domain;

    public class AddSatelliteCommand : IRequest<SatelliteSummaryViewModel>
    {
        public string SatelliteName { get; set; }
        public string SatelliteIP { get; set; }
    }
}
