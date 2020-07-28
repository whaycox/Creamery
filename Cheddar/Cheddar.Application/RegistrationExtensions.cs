using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Cheddar.Application
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddCheddarApplication(this IServiceCollection services, IConfiguration configuration) => services
            .AddMediatR(typeof(RegistrationExtensions).Assembly)
            .AddCheddarCore(configuration);
    }
}
