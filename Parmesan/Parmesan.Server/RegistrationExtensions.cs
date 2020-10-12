using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Curds.Persistence.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Parmesan.Server
{
    using Abstraction;
    using Application;
    using Domain;
    using Implementation;
    using Controllers.Domain;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanServer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(LoginCookieConstants.LoginAuthenticationScheme)
                .AddCookie(LoginCookieConstants.LoginAuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString(AuthenticationRoutes.LoginRoute);
                });

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
