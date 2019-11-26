using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Implementation
{
    using Abstraction;
    using Domain;
    using Gouda.Domain;

    public class SatelliteSummaryMapper : ISatelliteSummaryMapper
    {
        public SatelliteSummaryViewModel Map(Satellite entity) => new SatelliteSummaryViewModel
        {
            ID = entity.ID,
            Name = entity.Name,
            IPAddress = entity.IPAddress.ToString(),
            Status = entity.Status,
        };
    }
}
