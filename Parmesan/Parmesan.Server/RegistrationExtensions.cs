using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Curds.Persistence.Domain;

namespace Parmesan.Server
{
    using Abstraction;
    using Application;
    using Domain;
    using Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services
                .AddParmesanApplication()
                .Configure<OidcSettings>(configuration.GetSection("Parmesan.Server:OIDC"))
                .Configure<SqlConnectionInformation>(configuration.GetSection("Parmesan.Server:SQL"))
                .AddSingleton<IOidcProviderMetadataFactory, OidcProviderMetadataFactory>()
                .AddSingleton<IAuthorizationRequestParser, AuthorizationRequestParser>();

            return services;
        }
    }
}
