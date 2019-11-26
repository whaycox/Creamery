using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Queries.ListSatellites.Domain
{
    using ViewModels.Satellite.Domain;

    public class ListSatellitesResult
    {
        public SummaryCollectionViewModel Satellites { get; set; }
    }
}
