using Microsoft.Extensions.DependencyInjection;

namespace Curds
{
    using Clone.Abstraction;
    using Clone.Implementation;
    using Cron.Abstraction;
    using Cron.FieldDefinitions.Implementation;
    using Cron.FieldFactories.Implementation;
    using Cron.Implementation;
    using Cron.RangeFactories.Abstraction;
    using Cron.RangeFactories.Chains.Implementation;
    using Cron.RangeFactories.Implementation;
    using Expressions.Abstraction;
    using Expressions.Implementation;
    using Text.Abstraction;
    using Text.Implementation;
    using Time.Abstraction;
    using Time.Implementation;
    using Persistence.Abstraction;
    using Persistence.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddCurdsCore(this IServiceCollection services) => services
            .AddTime()
            .AddExpressions()
            .AddClone()
            .AddPersistence()
            .AddCron()
            .AddTransient<IIndentStringBuilder, IndentStringBuilder>();

        private static IServiceCollection AddTime(this IServiceCollection services) => services
            .AddSingleton<ITime, MachineTime>();

        private static IServiceCollection AddExpressions(this IServiceCollection services) => services
            .AddSingleton<IExpressionNodeFactory, ExpressionNodeFactory>()
            .AddSingleton<IExpressionFactory, ExpressionFactory>()
            .AddSingleton<IExpressionBuilderFactory, ExpressionBuilderFactory>()
            .AddSingleton<IExpressionVisitorFactory, ExpressionVisitorFactory>()
            .AddSingleton<IExpressionParser, ExpressionParser>();

        private static IServiceCollection AddClone(this IServiceCollection services) => services
            .AddSingleton<ICloneDefinitionFactory, CloneDefinitionFactory>()
            .AddSingleton<ICloneFactory, CloneFactory>();

        private static IServiceCollection AddPersistence(this IServiceCollection services) => services
            .AddSingleton<IEntityUpdateDelegateFactory, EntityUpdateDelegateFactory>()
            .AddSingleton(typeof(ISimpleRepository<>), typeof(CloningSimpleEntityRepository<>));

        private static IServiceCollection AddCron(this IServiceCollection services) => services
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
            .AddTransient<ICronFieldFactory<MinuteFieldDefinition>, MinuteFieldFactory>()
            .AddTransient<ICronFieldFactory<HourFieldDefinition>, HourFieldFactory>()
            .AddTransient<ICronFieldFactory<DayOfMonthFieldDefinition>, DayOfMonthFieldFactory>()
            .AddTransient<ICronFieldFactory<MonthFieldDefinition>, MonthFieldFactory>()
            .AddTransient<ICronFieldFactory<DayOfWeekFieldDefinition>, DayOfWeekFieldFactory>();
        private static IServiceCollection AddCronRangeFactories(this IServiceCollection services) => services
            .AddTransient<ICronRangeFactory<MinuteFieldDefinition>, MinuteRangeFactory>()
            .AddTransient<ICronRangeFactory<HourFieldDefinition>, HourRangeFactory>()
            .AddTransient<ICronRangeFactory<DayOfMonthFieldDefinition>, DayOfMonthRangeFactory>()
            .AddTransient<ICronRangeFactory<MonthFieldDefinition>, MonthRangeFactory>()
            .AddTransient<ICronRangeFactory<DayOfWeekFieldDefinition>, DayOfWeekRangeFactory>();
        private static IServiceCollection AddCronRangeLinkFactories(this IServiceCollection services) => services
            .AddTransient<IRangeFactoryChain<MinuteFieldDefinition>, MinuteChain>()
            .AddTransient<IRangeFactoryChain<HourFieldDefinition>, HourChain>()
            .AddTransient<IRangeFactoryChain<DayOfMonthFieldDefinition>, DayOfMonthChain>()
            .AddTransient<IRangeFactoryChain<MonthFieldDefinition>, MonthChain>()
            .AddTransient<IRangeFactoryChain<DayOfWeekFieldDefinition>, DayOfWeekChain>();
    }
}
