using MediatR;

namespace Cheddar.Application.Organization.Commands.Add.Domain
{
    public class AddOrganizationCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
