using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Parmesan.Server
{
    using Abstraction;
    using Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanServer(this IServiceCollection services)
        {
            services.AddControllersWithViews();

            services
                .AddSingleton<IOidcProviderMetadataFactory, OidcProviderMetadataFactory>();

            return services;
        }
    }
}
