using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Curds.Persistence.Domain;
using MediatR;

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
                .AddMediatR(typeof(RegistrationExtensions).Assembly)
                .AddParmesanApplication()
                .Configure<OidcSettings>(configuration.GetSection("Parmesan.Server:OIDC"))
                .Configure<SqlConnectionInformation>(configuration.GetSection("Parmesan.Server:SQL"))
                .AddSingleton<IOidcProviderMetadataFactory, OidcProviderMetadataFactory>()
                .AddSingleton<IAuthorizationRequestParser, AuthorizationRequestParser>();

            return services;
        }
    }
}
