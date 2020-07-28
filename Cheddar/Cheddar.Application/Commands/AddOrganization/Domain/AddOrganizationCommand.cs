using MediatR;

namespace Cheddar.Application.Commands.AddOrganization.Domain
{
    public class AddOrganizationCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
