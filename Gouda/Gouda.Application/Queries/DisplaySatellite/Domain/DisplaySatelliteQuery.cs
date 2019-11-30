using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Queries.DisplaySatellite.Domain
{
    public class DisplaySatelliteQuery : IRequest<DisplaySatelliteResult>
    {
        public int SatelliteID { get; set; }
    }
}
