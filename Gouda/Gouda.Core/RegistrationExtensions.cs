using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Curds;

namespace Gouda
{
    using Communication.Abstraction;
    using Communication.Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Persistence.Implementation;
    using Scheduling.Abstraction;
    using Scheduling.Implementation;
    using Analysis.Abstraction;
    using Analysis.Implementation;
    using Checks.Abstraction;
    using Checks.Implementation;
    using Domain;
    using Persistence.Repositories.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaCore(this IServiceCollection services, IConfiguration configuration) => services
            .AddCurdsCore()
            .AddSingleton<IScheduleFactory, ScheduleFactory>()
            .AddSingleton<IScheduler, Scheduler>()
            .AddTransient<ICommunicator, Communicator>()
            .AddTransient<IAnalyzer, Analyzer>()
            .AddDbContext<GoudaContext>(options => options.UseSqlServer(ConnectionString(configuration)))
            .AddTransient(typeof(IRepository<>), typeof(EFRepository<>))
            .AddTransient<IRepository<CheckDefinition>, CheckDefinitionRepository>()
            .AddTransient<IGoudaDatabase, EFGoudaDatabase>()
            .AddSingleton<ICheckInheritor, CheckInheritor>()
            .AddSingleton<ICheckLibrary, CheckLibrary>();

        private static string ConnectionString(IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection(PersistenceOptions.Key);
            PersistenceOptions options = section.Get<PersistenceOptions>();
            return $"Server={options.Server};Database={options.Database};Trusted_Connection=True;";
        }

    }
}
