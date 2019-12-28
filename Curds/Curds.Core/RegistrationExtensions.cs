using Microsoft.Extensions.DependencyInjection;

namespace Curds
{
    using Time.Abstraction;
    using Time.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddCurdsCore(this IServiceCollection services) => services
            .AddTransient<ITime, MachineTime>();
    }
}
