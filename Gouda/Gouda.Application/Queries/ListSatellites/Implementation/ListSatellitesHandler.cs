using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Gouda.Application.Queries.ListSatellites.Implementation
{
    using Domain;
    using Persistence.Abstraction;
    using Gouda.Domain;
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Domain;

    public class ListSatellitesHandler : IRequestHandler<ListSatellitesQuery, ListSatellitesResult>
    {
        private IGoudaDatabase GoudaDatabase { get; }
        private ISatelliteSummaryMapper SatelliteSummaryMapper { get; }

        public ListSatellitesHandler(IGoudaDatabase goudaDatabase, ISatelliteSummaryMapper satelliteSummaryMapper)
        {
            GoudaDatabase = goudaDatabase;
            SatelliteSummaryMapper = satelliteSummaryMapper;
        }

        public async Task<ListSatellitesResult> Handle(ListSatellitesQuery request, CancellationToken cancellationToken)
        {
            List<Satellite> satellites = await GoudaDatabase.Satellite.FetchAll();
            List<SatelliteSummaryViewModel> summaries = satellites
                .Select(satellite => SatelliteSummaryMapper.Map(satellite))
                .ToList();
            ListSatellitesResult result = new ListSatellitesResult
            {
                Satellites = new SummaryCollectionViewModel { Satellites = summaries },
            };

            return result;
        }
    }
}
