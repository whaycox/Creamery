using Microsoft.Extensions.DependencyInjection;

namespace Curds
{
    using Time.Abstraction;
    using Time.Implementation;
    using Cron.FieldDefinitions.Implementation;
    using Cron.Abstraction;
    using Cron.Implementation;
    using Cron.FieldFactories.Abstraction;
    using Cron.FieldFactories.Implementation;
    using Cron.RangeFactories.Abstraction;
    using Cron.RangeFactories.Implementation;
    using Cron.RangeFactories.Chains.Abstraction;
    using Cron.RangeFactories.Chains.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddCurdsCore(this IServiceCollection services) => services
            .AddTransient<ITime, MachineTime>();

        public static IServiceCollection AddCurdsCron(this IServiceCollection services) => services
            .AddTransient<ICronExpressionFactory, CronExpressionFactory>()
            .AddCronFieldDefinitions()
            .AddCronFieldFactories()
            .AddCronRangeFactories()
            .AddCronRangeLinkFactories();

        private static IServiceCollection AddCronFieldDefinitions(this IServiceCollection services) => services
            .AddSingleton<MinuteFieldDefinition>()
            .AddSingleton<HourFieldDefinition>()
            .AddSingleton<DayOfMonthFieldDefinition>()
            .AddSingleton<MonthFieldDefinition>()
            .AddSingleton<DayOfWeekFieldDefinition>();

        private static IServiceCollection AddCronFieldFactories(this IServiceCollection services) => services
            .AddTransient<IMinuteFieldFactory, MinuteFieldFactory>()
            .AddTransient<IHourFieldFactory, HourFieldFactory>()
            .AddTransient<IDayOfMonthFieldFactory, DayOfMonthFieldFactory>()
            .AddTransient<IMonthFieldFactory, MonthFieldFactory>()
            .AddTransient<IDayOfWeekFieldFactory, DayOfWeekFieldFactory>();

        private static IServiceCollection AddCronRangeFactories(this IServiceCollection services) => services
            .AddTransient<IMinuteRangeFactory, MinuteRangeFactory>()
            .AddTransient<IHourRangeFactory, HourRangeFactory>()
            .AddTransient<IDayOfMonthRangeFactory, DayOfMonthRangeFactory>()
            .AddTransient<IMonthRangeFactory, MonthRangeFactory>()
            .AddTransient<IDayOfWeekRangeFactory, DayOfWeekRangeFactory>();

        private static IServiceCollection AddCronRangeLinkFactories(this IServiceCollection services) => services
            .AddTransient<IMinuteRangeLinkFactory, MinuteRangeLinkFactory>()
            .AddTransient<IHourRangeLinkFactory, HourRangeLinkFactory>()
            .AddTransient<IDayOfMonthRangeLinkFactory, DayOfMonthRangeLinkFactory>()
            .AddTransient<IMonthRangeLinkFactory, MonthRangeLinkFactory>()
            .AddTransient<IDayOfWeekRangeLinkFactory, DayOfWeekRangeLinkFactory>();
    }
}
