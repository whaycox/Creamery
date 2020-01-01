using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Tests
{
    using Abstraction;
    using FieldDefinitions.Implementation;
    using FieldFactories.Implementation;
    using Implementation;
    using RangeFactories.Implementation;
    using RangeLinkFactories.Implementation;

    [TestClass]
    public class CronExpressionParsingTests
    {
        private MinuteFieldDefinition TestMinuteFieldDefinition = new MinuteFieldDefinition();
        private HourFieldDefinition TestHourFieldDefinition = new HourFieldDefinition();
        private DayOfMonthFieldDefinition TestDayOfMonthFieldDefinition = new DayOfMonthFieldDefinition();
        private MonthFieldDefinition TestMonthFieldDefinition = new MonthFieldDefinition();
        private DayOfWeekFieldDefinition TestDayOfWeekFieldDefinition = new DayOfWeekFieldDefinition();

        private MinuteFieldFactory TestMinuteFieldFactory = null;
        private HourFieldFactory TestHourFieldFactory = null;
        private DayOfMonthFieldFactory TestDayOfMonthFieldFactory = null;
        private MonthFieldFactory TestMonthFieldFactory = null;
        private DayOfWeekFieldFactory TestDayOfWeekFieldFactory = null;

        private MinuteRangeFactory TestMinuteRangeFactory = null;
        private HourRangeFactory TestHourRangeFactory = null;
        private DayOfMonthRangeFactory TestDayOfMonthRangeFactory = null;
        private MonthRangeFactory TestMonthRangeFactory = null;
        private DayOfWeekRangeFactory TestDayOfWeekRangeFactory = null;

        private MinuteRangeLinkFactory TestMinuteRangeLinkFactory = null;
        private HourRangeLinkFactory TestHourRangeLinkFactory = null;
        private DayOfMonthRangeLinkFactory TestDayOfMonthRangeLinkFactory = null;
        private MonthRangeLinkFactory TestMonthRangeLinkFactory = null;
        private DayOfWeekRangeLinkFactory TestDayOfWeekRangeLinkFactory = null;

        private CronExpressionFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestMinuteRangeLinkFactory = new MinuteRangeLinkFactory(TestMinuteFieldDefinition);
            TestHourRangeLinkFactory = new HourRangeLinkFactory(TestHourFieldDefinition);
            TestDayOfMonthRangeLinkFactory = new DayOfMonthRangeLinkFactory(TestDayOfMonthFieldDefinition);
            TestMonthRangeLinkFactory = new MonthRangeLinkFactory(TestMonthFieldDefinition);
            TestDayOfWeekRangeLinkFactory = new DayOfWeekRangeLinkFactory(TestDayOfWeekFieldDefinition);

            TestMinuteRangeFactory = new MinuteRangeFactory(TestMinuteRangeLinkFactory);
            TestHourRangeFactory = new HourRangeFactory(TestHourRangeLinkFactory);
            TestDayOfMonthRangeFactory = new DayOfMonthRangeFactory(TestDayOfMonthRangeLinkFactory);
            TestMonthRangeFactory = new MonthRangeFactory(TestMonthRangeLinkFactory);
            TestDayOfWeekRangeFactory = new DayOfWeekRangeFactory(TestDayOfWeekRangeLinkFactory);

            TestMinuteFieldFactory = new MinuteFieldFactory(TestMinuteRangeFactory);
            TestHourFieldFactory = new HourFieldFactory(TestHourRangeFactory);
            TestDayOfMonthFieldFactory = new DayOfMonthFieldFactory(TestDayOfMonthRangeFactory);
            TestMonthFieldFactory = new MonthFieldFactory(TestMonthRangeFactory);
            TestDayOfWeekFieldFactory = new DayOfWeekFieldFactory(TestDayOfWeekRangeFactory);

            TestObject = new CronExpressionFactory(
                TestMinuteFieldFactory,
                TestHourFieldFactory,
                TestDayOfMonthFieldFactory,
                TestMonthFieldFactory,
                TestDayOfWeekFieldFactory);
        }

        [TestMethod]
        public void CanParseSingleValueExpressions()
        {
            ICronExpression expression = TestObject.Parse("1 1 1 1 1");

            DateTime testTime = new DateTime(1, 1, 1, 1, 0, 0);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddMinutes(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddMinutes(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseSingleValueExpressionsWithMonthAliases()
        {
            ICronExpression expression = TestObject.Parse("* * * APR *");

            DateTime testTime = new DateTime(1, 3, 31);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseSingleValueExpressionsWithDayOfWeekAliases()
        {
            ICronExpression expression = TestObject.Parse("* * * * TUE");

            DateTime testTime = new DateTime(2019, 12, 23);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseWildcardExpressions()
        {
            ICronExpression expression = TestObject.Parse("* * * * *");

            Assert.IsTrue(expression.IsActive(DateTime.MinValue));
            Assert.IsTrue(expression.IsActive(DateTime.MaxValue));
        }

        [TestMethod]
        public void CanParseWildcardsWithStepRanges()
        {
            ICronExpression expression = TestObject.Parse("*/3 * * * *");

            DateTime testTime = DateTime.MinValue;
            for (int i = 0; i < 10; i++, testTime = testTime.AddMinutes(1))
            {
                if (i % 3 == 0)
                    Assert.IsTrue(expression.IsActive(testTime));
                else
                    Assert.IsFalse(expression.IsActive(testTime));
            }
        }

        [TestMethod]
        public void CanParseRangeExpressions()
        {
            ICronExpression expression = TestObject.Parse("1-2 1-2 1-2 1-2 1-2");

            DateTime testTime = new DateTime(1, 1, 1, 1, 0, 0);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddMinutes(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddMinutes(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddMinutes(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseRangeExpressionsWithMonthAliases()
        {
            ICronExpression expression = TestObject.Parse("* * * JAN-MAR *");

            DateTime testTime = new DateTime(1, 3, 31);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseRangeExpressionsWithDayOfWeekAliases()
        {
            ICronExpression expression = TestObject.Parse("* * * * THU-SAT");

            DateTime testTime = new DateTime(2019, 12, 28);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseExpressionsWithNthDayOfWeek()
        {
            ICronExpression expression = TestObject.Parse("* * * * 4#2");

            DateTime testTime = new DateTime(2019, 12, 11);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }
    }
}
