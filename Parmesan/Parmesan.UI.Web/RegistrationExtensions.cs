using Microsoft.Extensions.DependencyInjection;

namespace Parmesan.UI.Web
{
    using Abstraction;
    using Implementation;
    using Parmesan.Abstraction;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanWebUI(this IServiceCollection services, string clientName) => services
            .AddSingleton<ISecureRandom, SecureRandom>()
            .AddSingleton<IClientIDFactory>(new ClientIDFactory(clientName))
            .AddSingleton<IStateFactory, StateFactory>()
            .AddSingleton<IPkceFactory, PkceFactory>()
            .AddSingleton<IRemoteOidcMetadataProvider, RemoteOidcMetadataProvider>()
            .AddSingleton<IAccessTokenProvider, AccessTokenProvider>()
            .AddSingleton<ILoginRequestFactory, LoginRequestFactory>()
            .AddSingleton<ILoginRequestStorage, LoginRequestStorage>();
    }
}
