using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Parmesan.Application
{
    using Abstraction;
    using Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanApplication(this IServiceCollection services) => services
            .AddParmesanCore()
            .AddMediatR(typeof(RegistrationExtensions))
            .AddSingleton<IAuthorizationTicketRepository, AuthorizationTicketRepository>();
    }
}
