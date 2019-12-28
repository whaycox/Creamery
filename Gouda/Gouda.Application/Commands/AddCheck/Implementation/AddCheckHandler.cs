using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.AddCheck.Implementation
{
    using Domain;
    using ViewModels.Satellite.Domain;
    using Persistence.Abstraction;
    using Gouda.Domain;
    using ViewModels.Satellite.Abstraction;

    public class AddCheckHandler : IRequestHandler<AddCheckCommand, CheckViewModel>
    {
        private IGoudaDatabase GoudaDatabase { get; }
        private ICheckDefinitionMapper ViewModelMapper { get; }

        public AddCheckHandler(
            IGoudaDatabase goudaDatabase,
            ICheckDefinitionMapper viewModelMapper)
        {
            GoudaDatabase = goudaDatabase;
            ViewModelMapper = viewModelMapper;
        }

        public async Task<CheckViewModel> Handle(AddCheckCommand request, CancellationToken cancellationToken)
        {
            CheckDefinition addedCheckDefinition = BuildCheckDefinition(request);
            await GoudaDatabase.CheckDefinition.Insert(addedCheckDefinition);
            return ViewModelMapper.Map(addedCheckDefinition);
        }
        private CheckDefinition BuildCheckDefinition(AddCheckCommand command) => new CheckDefinition
        {
            Name = command.Name,
            CheckID = command.CheckID,
            SatelliteID = command.SatelliteID,
        };

    }
}
