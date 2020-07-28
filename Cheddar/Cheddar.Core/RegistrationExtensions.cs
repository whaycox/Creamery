using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Curds.Persistence;
using Microsoft.Extensions.Options;
using Curds.Persistence.Domain;
using Microsoft.Extensions.Configuration;

namespace Cheddar
{
    using Persistence.Abstraction;
    using Persistence.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddCheddarCore(this IServiceCollection services, IConfiguration configuration) => services
            .Configure<SqlConnectionInformation>(configuration.GetSection("Cheddar:Persistence:Connection"))
            .AddCurdsPersistence()
            .AddTransient<ICheddarDatabase, CheddarDatabase>()
            .ConfigureDataModel();

        private static IServiceCollection ConfigureDataModel(this IServiceCollection services) => services
            .ConfigureDefaultSchema(nameof(Cheddar));
    }
}
