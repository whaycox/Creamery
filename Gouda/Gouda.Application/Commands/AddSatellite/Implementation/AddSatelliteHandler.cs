using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Gouda.Application.Commands.AddSatellite.Implementation
{
    using Domain;
    using Persistence.Abstraction;
    using Gouda.Domain;
    using ViewModels.Satellite.Domain;
    using ViewModels.Satellite.Abstraction;

    public class AddSatelliteHandler : IRequestHandler<AddSatelliteCommand, AddSatelliteResult>
    {
        private IGoudaDatabase GoudaDatabase { get; }
        private ISatelliteSummaryMapper SummaryMapper { get; }

        public AddSatelliteHandler(IGoudaDatabase goudaDatabase, ISatelliteSummaryMapper summaryMapper)
        {
            GoudaDatabase = goudaDatabase;
            SummaryMapper = summaryMapper;
        }

        public async Task<AddSatelliteResult> Handle(AddSatelliteCommand request, CancellationToken cancellationToken)
        {
            Satellite newSatellite = BuildNewSatellite(request);
            await GoudaDatabase.Satellite.Insert(newSatellite);
            await GoudaDatabase.SaveChanges();
            return new AddSatelliteResult { NewSatellite = SummaryMapper.Map(newSatellite) };
        }
        private Satellite BuildNewSatellite(AddSatelliteCommand command) => new Satellite
        {
            Name = command.SatelliteName,
            IPAddress = IPAddress.Parse(command.SatelliteIP),
        };
    }
}
