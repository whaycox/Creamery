using MediatR;
using System;

namespace Gouda.Application.Commands.AddCheck.Domain
{
    using ViewModels.Satellite.Domain;

    public class AddCheckCommand : IRequest<CheckViewModel>
    {
        public int SatelliteID { get; set; }
        public string Name { get; set; }
        public Guid CheckID { get; set; }
        public int? RescheduleInterval { get; set; }
    }
}
