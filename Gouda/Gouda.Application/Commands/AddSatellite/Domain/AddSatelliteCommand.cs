using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Commands.AddSatellite.Domain
{
    public class AddSatelliteCommand : IRequest<AddSatelliteResult>
    {
        public string SatelliteName { get; set; }
        public string SatelliteIP { get; set; }
    }
}
