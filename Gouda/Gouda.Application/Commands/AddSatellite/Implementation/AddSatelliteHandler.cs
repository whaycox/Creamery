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

    public class AddSatelliteHandler : IRequestHandler<AddSatelliteCommand, AddSatelliteResult>
    {
        private IGoudaDatabase GoudaDatabase { get; }

        public AddSatelliteHandler(IGoudaDatabase goudaDatabase)
        {
            GoudaDatabase = goudaDatabase;
        }

        public async Task<AddSatelliteResult> Handle(AddSatelliteCommand request, CancellationToken cancellationToken)
        {
            Satellite newSatellite = BuildNewSatellite(request);
            await GoudaDatabase.Satellite.Insert(newSatellite);
            await GoudaDatabase.SaveChanges();
            return new AddSatelliteResult { NewSatellite = BuildViewModel(newSatellite) };
        }
        private Satellite BuildNewSatellite(AddSatelliteCommand command) => new Satellite
        {
            Name = command.SatelliteName,
            IPAddress = IPAddress.Parse(command.SatelliteIP),
        };
        private SatelliteSummaryViewModel BuildViewModel(Satellite newSatellite) => new SatelliteSummaryViewModel
        {
            ID = newSatellite.ID,
            Name = newSatellite.Name,
            IPAddress = newSatellite.IPAddress.ToString(),
            Status = newSatellite.Status,
        };
    }
}
