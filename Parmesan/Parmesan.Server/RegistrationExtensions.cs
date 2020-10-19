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

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanServer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(ServerConstants.LoginAuthorizationPolicy, builder =>
                    {
                        builder.AuthenticationSchemes.Add(ServerConstants.LoginAuthenticationScheme);
                        builder.RequireAuthenticatedUser();
                    });
                })
                .AddAuthentication()
                .AddCookie(ServerConstants.LoginAuthenticationScheme, options =>
                {
                    options.Cookie = new CookieBuilder
                    {
                        IsEssential = true,
                        SameSite = SameSiteMode.Strict,
                        Path = ServerConstants.CookiePath,
                    };
                    options.LoginPath = new PathString(ServerConstants.LoginRoute);
                });

            services.AddControllersWithViews();

            services
                .AddMediatR(typeof(RegistrationExtensions).Assembly)
                .AddParmesanApplication()
                .Configure<ServerOptions>(configuration.GetSection("Parmesan.Server"))
                .Configure<OidcSettings>(configuration.GetSection("Parmesan.Server:OIDC"))
                .Configure<SqlConnectionInformation>(configuration.GetSection("Parmesan.Server:SQL"))
                .AddSingleton<IOidcProviderMetadataFactory, OidcProviderMetadataFactory>()
                .AddSingleton<IAuthorizationRequestParser, AuthorizationRequestParser>();

            return services;
        }
    }
}
