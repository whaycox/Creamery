using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Gouda
{
    using Communication.Abstraction;
    using Communication.Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Persistence.Implementation;
    using Scheduling.Abstraction;
    using Scheduling.Implementation;
    using Time.Abstraction;
    using Time.Implementation;
    using Analysis.Abstraction;
    using Analysis.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaCore(this IServiceCollection services) => services
            .AddTransient<ITime, MachineTime>()
            .AddSingleton<IScheduleFactory, ScheduleFactory>()
            .AddSingleton<IScheduler, Scheduler>()
            .AddTransient<ICommunicator, Communicator>()
            .AddTransient<IAnalyzer, Analyzer>()
            .AddDbContext<GoudaContext>(options => options.UseInMemoryDatabase(nameof(Gouda)))
            .AddTransient(typeof(IRepository<>), typeof(EFRepository<>))
            .AddTransient<IGoudaDatabase, EFGoudaDatabase>()
            .AddSingleton<ICheckInheritor, CheckInheritor>();
    }
}
