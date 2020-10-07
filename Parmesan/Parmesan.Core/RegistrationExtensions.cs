using System;
using Microsoft.Extensions.DependencyInjection;
using Curds.Persistence;

namespace Parmesan
{
    using Persistence.Abstraction;
    using Persistence.Implementation;
    using Domain;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanCore(this IServiceCollection services) => services
            .AddCurdsPersistence()
            .ConfigureParmesanDataModel()
            .AddTransient<IParmesanDatabase, SqlParmesanDatabase>()
            .AddTransient<IClientRepository, SqlClientRepository>();

        private static IServiceCollection ConfigureParmesanDataModel(this IServiceCollection services) => services
            .ConfigureDefaultSchema(nameof(Parmesan));
    }
}
