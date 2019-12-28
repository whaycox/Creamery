using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Queries.DisplaySatellite.Domain
{
    using ViewModels.Satellite.Domain;

    public class DisplaySatelliteQuery : IRequest<SatelliteViewModel>
    {
        public int SatelliteID { get; set; }
    }
}
