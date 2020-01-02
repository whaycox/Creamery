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
    using Cron.FieldFactories.Abstraction;
    using Cron.FieldFactories.Implementation;
    using Cron.RangeFactories.Abstraction;
    using Cron.RangeFactories.Implementation;
    using Cron.RangeLinkFactories.Abstraction;
    using Cron.RangeLinkFactories.Implementation;

    [TestClass]
    public class RegistrationExtensionsTest : RegistrationExtensionsTemplate
    {
        private void AddCurdsCore() => TestServiceCollection.AddCurdsCore();
        private void AddCurdsCron() => TestServiceCollection.AddCurdsCron();

        [TestMethod]
        public void CurdsCoreAddsTime()
        {
            AddCurdsCore();

            VerifyServiceWasRegistered(typeof(ITime), typeof(MachineTime), ServiceLifetime.Transient);
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

            VerifyServiceWasRegistered(typeof(IMinuteFieldFactory), typeof(MinuteFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IHourFieldFactory), typeof(HourFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IDayOfMonthFieldFactory), typeof(DayOfMonthFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IMonthFieldFactory), typeof(MonthFieldFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IDayOfWeekFieldFactory), typeof(DayOfWeekFieldFactory), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void CurdsCronAddsRangeFactories()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(IMinuteRangeFactory), typeof(MinuteRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IHourRangeFactory), typeof(HourRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IDayOfMonthRangeFactory), typeof(DayOfMonthRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IMonthRangeFactory), typeof(MonthRangeFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IDayOfWeekRangeFactory), typeof(DayOfWeekRangeFactory), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void CurdsCronAddsRangeLinkFactories()
        {
            AddCurdsCron();

            VerifyServiceWasRegistered(typeof(IMinuteRangeLinkFactory), typeof(MinuteRangeLinkFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IHourRangeLinkFactory), typeof(HourRangeLinkFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IDayOfMonthRangeLinkFactory), typeof(DayOfMonthRangeLinkFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IMonthRangeLinkFactory), typeof(MonthRangeLinkFactory), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(IDayOfWeekRangeLinkFactory), typeof(DayOfWeekRangeLinkFactory), ServiceLifetime.Transient);
        }
    }
}
