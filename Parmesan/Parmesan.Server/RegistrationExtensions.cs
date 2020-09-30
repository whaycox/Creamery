using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Parmesan.Server
{
    using Abstraction;
    using Domain;
    using Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services
                .Configure<OidcSettings>(configuration.GetSection("Parmesan.Server:OIDC"))
                .AddSingleton<IOidcProviderMetadataFactory, OidcProviderMetadataFactory>()
                .AddSingleton<IAuthorizationRequestParser, AuthorizationRequestParser>();

            return services;
        }
    }
}
