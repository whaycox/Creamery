using Microsoft.Extensions.DependencyInjection;

namespace Gouda
{
    using Communication.Abstraction;
    using Communication.Implementation;
    using Persistence.Abstraction;
    using Persistence.Implementation;
    using Scheduling.Abstraction;
    using Scheduling.Implementation;
    using Time.Abstraction;
    using Time.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaCore(this IServiceCollection services) => services
            .AddTransient<ITime, MachineTime>()
            .AddSingleton<IScheduler, Scheduler>()
            .AddTransient<ICommunicator, Communicator>()
            .AddTransient<IGoudaDatabase, EFGoudaDatabase>();
    }
}
