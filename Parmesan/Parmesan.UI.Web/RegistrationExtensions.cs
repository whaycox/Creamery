using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Parmesan.UI.Web
{
    using Abstraction;
    using Domain;
    using Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanWebUI(this IServiceCollection services, string clientName) => services
            .AddSingleton<IClientIDFactory>(new ClientIDFactory(clientName))
            .AddSingleton<IStateFactory, StateFactory>()
            .AddSingleton<IPkceFactory, PkceFactory>()
            .AddSingleton<IRemoteOidcMetadataProvider, RemoteOidcMetadataProvider>()
            .AddSingleton<ILoginRequestFactory, LoginRequestFactory>();
    }
}
