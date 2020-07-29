using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cheddar.Application.Organization.Commands.Add.Implementation
{
    using Cheddar.Domain;
    using Domain;
    using Persistence.Abstraction;

    internal class AddOrganizationHandler : IRequestHandler<AddOrganizationCommand, int>
    {
        private ICheddarDatabase Database { get; }

        public AddOrganizationHandler(ICheddarDatabase database)
        {
            Database = database;
        }

        public async Task<int> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
        {
            Organization newOrganization = BuildNewOrganization(request);
            await Database.Organizations.Insert(newOrganization);
            return newOrganization.ID;
        }
        private Organization BuildNewOrganization(AddOrganizationCommand command) => new Organization
        {
            Name = command.Name,
        };
    }
}
