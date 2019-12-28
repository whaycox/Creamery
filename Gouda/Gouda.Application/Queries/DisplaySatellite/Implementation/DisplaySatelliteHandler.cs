﻿using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gouda.Application.Queries.DisplaySatellite.Implementation
{
    using Domain;
    using Persistence.Abstraction;
    using Gouda.Domain;
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Domain;

    public class DisplaySatelliteHandler : IRequestHandler<DisplaySatelliteQuery, SatelliteViewModel>
    {
        private IGoudaDatabase GoudaDatabase { get; }
        private ISatelliteMapper SatelliteMapper { get; }

        public DisplaySatelliteHandler(IGoudaDatabase goudaDatabase, ISatelliteMapper satelliteMapper)
        {
            GoudaDatabase = goudaDatabase;
            SatelliteMapper = satelliteMapper;
        }

        public async Task<SatelliteViewModel> Handle(DisplaySatelliteQuery request, CancellationToken cancellationToken)
        {
            Satellite satellite = await GoudaDatabase.Satellite.Fetch(request.SatelliteID);
            return SatelliteMapper.Map(satellite);
        }
    }
}
