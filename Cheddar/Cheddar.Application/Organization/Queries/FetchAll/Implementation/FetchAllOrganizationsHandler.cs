using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cheddar.Application.Organization.Queries.FetchAll.Implementation
{
    using Cheddar.Domain;
    using Domain;
    using Persistence.Abstraction;
    using ViewModel.Domain;

    internal class FetchAllOrganizationsHandler : IRequestHandler<FetchAllOrganizationsQuery, IEnumerable<OrganizationViewModel>>
    {
        private ICheddarDatabase Database { get; }

        public FetchAllOrganizationsHandler(ICheddarDatabase database)
        {
            Database = database;
        }

        public async Task<IEnumerable<OrganizationViewModel>> Handle(
            FetchAllOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            List<Organization> organizations = await Database.Organizations.FetchAll();
            return organizations.Select(organization => BuildViewModel(organization));
        }
        private OrganizationViewModel BuildViewModel(Organization organization) => new OrganizationViewModel
        {
            Name = organization.Name,
        };
    }
}
