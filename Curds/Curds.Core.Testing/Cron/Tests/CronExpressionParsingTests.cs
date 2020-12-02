using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Tests
{
    using Abstraction;

    [TestClass]
    public class CronExpressionParsingTests
    {
        private IServiceProvider ServiceProvider = null;

        private ICronExpressionFactory TestObject => ServiceProvider.GetService<ICronExpressionFactory>();

        [TestInitialize]
        public void Init()
        {
            ServiceProvider = new ServiceCollection()
                .AddCurdsCore()
                .BuildServiceProvider();
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

        [TestMethod]
        public void CanParseExpressionsWithLastDayOfWeek()
        {
            ICronExpression expression = TestObject.Parse("* * * * 0L");

            DateTime testTime = new DateTime(2019, 12, 1);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsTrue(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseExpressionsWithNearestWeekday()
        {
            ICronExpression expression = TestObject.Parse("* * 12W * *");

            DateTime testTime = new DateTime(2020, 1, 12);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseExpressionsWithLastDayOfMonth()
        {
            ICronExpression expression = TestObject.Parse("* * L * *");

            DateTime testTime = new DateTime(2020, 2, 28);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void CanParseExpressionsWithOffsetLastDayOfMonth()
        {
            ICronExpression expression = TestObject.Parse("* * L-5 * *");

            DateTime testTime = new DateTime(2020, 2, 23);
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }


        [TestMethod]
        public void LastFridayEveryQuarter()
        {
            ICronExpression expression = TestObject.Parse("* * * MAR,JUN,SEP,DEC FRIL");

            VerifyThreeConsecutiveDates(expression, new DateTime(2019, 3, 28));
            VerifyThreeConsecutiveDates(expression, new DateTime(2019, 6, 27));
            VerifyThreeConsecutiveDates(expression, new DateTime(2019, 9, 26));
            VerifyThreeConsecutiveDates(expression, new DateTime(2019, 12, 26));
        }
        private void VerifyThreeConsecutiveDates(ICronExpression expression, DateTime testTime)
        {
            Assert.IsFalse(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(expression.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(expression.IsActive(testTime));
        }

        [TestMethod]
        public void EveryOtherHourOnLastDayOfMonth()
        {
            ICronExpression expression = TestObject.Parse("0 */2 L * *");

            VerifyEveryOtherHour(expression, new DateTime(2019, 1, 31));
            VerifyEveryOtherHour(expression, new DateTime(2019, 2, 28));
            VerifyEveryOtherHour(expression, new DateTime(2019, 4, 30));
        }
        private void VerifyEveryOtherHour(ICronExpression expression, DateTime testTime)
        {
            for (int i = 0; i < 24; i++, testTime = testTime.AddHours(1))
            {
                if (i % 2 == 0)
                    Assert.IsTrue(expression.IsActive(testTime));
                else
                    Assert.IsFalse(expression.IsActive(testTime));
            }
        }
    }
}
