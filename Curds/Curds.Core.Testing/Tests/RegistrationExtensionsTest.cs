using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whey.Template;
using System;

namespace Curds.Tests
{
    using Time.Abstraction;
    using Time.Implementation;
    using Cron.FieldDefinitions.Implementation;
    using Cron.Abstraction;
    using Cron.Implementation;
    using Cron.FieldFactories.Implementation;
    using Cron.RangeFactories.Abstraction;
    using Cron.RangeFactories.Implementation;
    using Cron.RangeFactories.Chains.Implementation;
    using Text.Abstraction;
    using Text.Implementation;

    [TestClass]
    public class RegistrationExtensionsTest : RegistrationExtensionsTemplate
    {
        private void AddCurdsCore() => TestServiceCollection.AddCurdsCore();
        private void AddCurdsCron() => TestServiceCollection.AddCurdsCron();

        [TestMethod]
        public void CurdsCoreAddsTime()
        {
            AddCurdsCore();

            VerifyServiceWasRegistered(typeof(ITime), typeof(MachineTime), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void CurdsCoreAddsIndentStringBuilder()
        {
            AddCurdsCore();

            VerifyServiceWasRegistered(typeof(IIndentStringBuilder), typeof(IndentStringBuilder), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void CurdsCronAddsExpressionFactory()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(ICronExpressionFactory), typeof(CronExpressionFactory), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void CurdsCronAddsFieldDefinitions()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(MinuteFieldDefinition), typeof(MinuteFieldDefinition), ServiceLifetime.Singleton);
            VerifyServiceWasRegistered(typeof(HourFieldDefinition), typeof(HourFieldDefinition), ServiceLifetime.Singleton);
            VerifyServiceWasRegistered(typeof(DayOfMonthFieldDefinition), typeof(DayOfMonthFieldDefinition), ServiceLifetime.Singleton);
            VerifyServiceWasRegistered(typeof(MonthFieldDefinition), typeof(MonthFieldDefinition), ServiceLifetime.Singleton);
            VerifyServiceWasRegistered(typeof(DayOfWeekFieldDefinition), typeof(DayOfWeekFieldDefinition), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void CurdsCronAddsFieldFactories()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(ICronFieldFactory<MinuteFieldDefinition>), typeof(MinuteFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronFieldFactory<HourFieldDefinition>), typeof(HourFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronFieldFactory<DayOfMonthFieldDefinition>), typeof(DayOfMonthFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronFieldFactory<MonthFieldDefinition>), typeof(MonthFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronFieldFactory<DayOfWeekFieldDefinition>), typeof(DayOfWeekFieldFactory), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void CurdsCronAddsRangeFactories()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(ICronRangeFactory<MinuteFieldDefinition>), typeof(MinuteRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronRangeFactory<HourFieldDefinition>), typeof(HourRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronRangeFactory<DayOfMonthFieldDefinition>), typeof(DayOfMonthRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronRangeFactory<MonthFieldDefinition>), typeof(MonthRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICronRangeFactory<DayOfWeekFieldDefinition>), typeof(DayOfWeekRangeFactory), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void CurdsCronAddsRangeLinkFactories()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(IRangeFactoryChain<MinuteFieldDefinition>), typeof(MinuteChain), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IRangeFactoryChain<HourFieldDefinition>), typeof(HourChain), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IRangeFactoryChain<DayOfMonthFieldDefinition>), typeof(DayOfMonthChain), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IRangeFactoryChain<MonthFieldDefinition>), typeof(MonthChain), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IRangeFactoryChain<DayOfWeekFieldDefinition>), typeof(DayOfWeekChain), ServiceLifetime.Transient);
        }
    }
}
