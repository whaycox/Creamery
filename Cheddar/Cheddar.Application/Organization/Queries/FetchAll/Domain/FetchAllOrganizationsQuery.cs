using MediatR;
using System.Collections.Generic;

namespace Cheddar.Application.Organization.Queries.FetchAll.Domain
{
    using ViewModel.Domain;

    public class FetchAllOrganizationsQuery : IRequest<IEnumerable<OrganizationViewModel>>
    { }
}
