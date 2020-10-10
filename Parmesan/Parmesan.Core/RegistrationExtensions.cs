using Curds.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Parmesan
{
    using Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanCore(this IServiceCollection services) => services
            .AddCurdsPersistence()
            .ConfigureParmesanDataModel()
            .AddTransient<IParmesanDatabase, SqlParmesanDatabase>()
            .AddTransient<IScopeResolver, ScopeResolver>()
            .AddTransient<IClientRepository, SqlClientRepository>();

        private static IServiceCollection ConfigureParmesanDataModel(this IServiceCollection services) => services
            .ConfigureDefaultSchema(nameof(Parmesan));
    }
}
