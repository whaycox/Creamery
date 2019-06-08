using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Template
{
    using Domain;

    public abstract class CronExpression<T> : Test where T : Implementation.CronExpression
    {
        protected abstract T BuildExpression(string expression);

        protected void TestCase(ExpressionCase expressionCase)
        {
            T expression = BuildExpression(expressionCase.Expression);
            foreach (DateTime time in expressionCase.TrueTimes)
                Assert.IsTrue(expression.Test(time));
            foreach (DateTime time in expressionCase.FalseTimes)
                Assert.IsFalse(expression.Test(time));
        }

        #region DayOfMonth

        [TestMethod]
        public void EveryMonthFifthThroughThirteenth() => TestCase(new EveryMonthFifthThroughThirteenthCase());
        private class EveryMonthFifthThroughThirteenthCase : ExpressionCase
        {
            public override string Expression => "* * 5-13 * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 05, 05),
                new DateTime(2018, 05, 06),
                new DateTime(2018, 05, 07),
                new DateTime(2018, 05, 08),
                new DateTime(2018, 05, 09),
                new DateTime(2018, 05, 10),
                new DateTime(2018, 05, 11),
                new DateTime(2018, 05, 12),
                new DateTime(2018, 05, 13),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 05, 01),
                new DateTime(2018, 05, 02),
                new DateTime(2018, 05, 03),
                new DateTime(2018, 05, 04),
                new DateTime(2018, 05, 14),
                new DateTime(2018, 05, 15),
                new DateTime(2018, 05, 16),
                new DateTime(2018, 05, 17),
                new DateTime(2018, 05, 18),
            };
        }

        [TestMethod]
        public void LastDayOfEveryMonth() => TestCase(new LastDayOfEveryMonthCase());
        private class LastDayOfEveryMonthCase : ExpressionCase
        {
            public override string Expression => "* * L * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 02, 28),
                new DateTime(2016, 02, 29),
                new DateTime(2018, 03, 31),
                new DateTime(2018, 04, 30),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 02, 27),
                new DateTime(2016, 02, 28),
                new DateTime(2018, 03, 30),
                new DateTime(2018, 04, 29),
            };
        }

        [TestMethod]
        public void WeekdayNearestFifteenthEveryMonth() => TestCase(new WeekdayNearestFifteenthEveryMonthCase());
        private class WeekdayNearestFifteenthEveryMonthCase : ExpressionCase
        {
            public override string Expression => "* * 15W * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 11, 15),
                new DateTime(2018, 09, 14),
                new DateTime(2018, 07, 16),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 11, 14),
                new DateTime(2018, 11, 16),
                new DateTime(2018, 09, 17),
                new DateTime(2018, 07, 13),
            };
        }

        [TestMethod]
        public void OffsetLastDayOfEveryMonth() => TestCase(new OffsetLastDayOfEveryMonthCase());
        private class OffsetLastDayOfEveryMonthCase : ExpressionCase
        {
            public override string Expression => "* * L-5 * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 07, 26),
                new DateTime(2018, 06, 25),
                new DateTime(2018, 02, 23),
                new DateTime(2016, 02, 24),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 07, 27),
                new DateTime(2018, 07, 25),
                new DateTime(2018, 07, 31),
                new DateTime(2018, 06, 26),
                new DateTime(2018, 06, 24),
                new DateTime(2018, 06, 30),
                new DateTime(2018, 02, 22),
                new DateTime(2018, 02, 24),
                new DateTime(2018, 02, 28),
                new DateTime(2016, 02, 23),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 29),
            };
        }

        [TestMethod]
        public void TenthTwentiethLastEveryMonth() => TestCase(new TenthTwentiethLastEveryMonthCase());
        private class TenthTwentiethLastEveryMonthCase : ExpressionCase
        {
            public override string Expression => "* * 10,20,L * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 04, 10),
                new DateTime(2018, 04, 20),
                new DateTime(2018, 04, 30),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 04, 08),
                new DateTime(2018, 04, 09),
                new DateTime(2018, 04, 11),
                new DateTime(2018, 04, 12),
                new DateTime(2018, 04, 18),
                new DateTime(2018, 04, 19),
                new DateTime(2018, 04, 21),
                new DateTime(2018, 04, 22),
                new DateTime(2018, 04, 28),
                new DateTime(2018, 04, 29),
            };
        }

        #endregion

        #region DayOfWeek

        [TestMethod]
        public void LastWednesdayEveryMonth() => TestCase(new LastWednesdayEveryMonthCase());
        private class LastWednesdayEveryMonthCase : ExpressionCase
        {
            public override string Expression => "* * * * WEDL";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 11, 28),
                new DateTime(2018, 10, 31),
                new DateTime(2018, 12, 26),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 11, 07),
                new DateTime(2018, 11, 14),
                new DateTime(2018, 11, 21),
                new DateTime(2018, 10, 03),
                new DateTime(2018, 10, 10),
                new DateTime(2018, 10, 17),
                new DateTime(2018, 10, 24),
                new DateTime(2018, 12, 05),
                new DateTime(2018, 12, 12),
                new DateTime(2018, 12, 19),
            };
        }

        [TestMethod]
        public void EveryMonWedFri() => TestCase(new EveryMonWedFriCase());
        private class EveryMonWedFriCase : ExpressionCase
        {
            public override string Expression => "* * * * Mon,Wed,5";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 02, 01),
                new DateTime(2017, 02, 03),
                new DateTime(2017, 02, 06),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 02, 02),
                new DateTime(2017, 02, 04),
                new DateTime(2017, 02, 05),
                new DateTime(2017, 02, 07),
            };
        }

        [TestMethod]
        public void ThirdThursdayEveryMonth() => TestCase(new ThirdThursdayEveryMonthCase());
        private class ThirdThursdayEveryMonthCase : ExpressionCase
        {
            public override string Expression => "* * * * Thu#3";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2018, 11, 15),
                new DateTime(2018, 10, 18),
                new DateTime(2018, 09, 20),
                new DateTime(2018, 08, 16),
                new DateTime(2018, 07, 19),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2018, 11, 08),
                new DateTime(2018, 11, 22),
                new DateTime(2018, 11, 16),
                new DateTime(2018, 11, 14),
                new DateTime(2018, 10, 11),
                new DateTime(2018, 10, 25),
                new DateTime(2018, 10, 17),
                new DateTime(2018, 10, 19),
                new DateTime(2018, 09, 13),
                new DateTime(2018, 09, 27),
                new DateTime(2018, 09, 19),
                new DateTime(2018, 09, 21),
                new DateTime(2018, 08, 09),
                new DateTime(2018, 08, 23),
                new DateTime(2018, 08, 15),
                new DateTime(2018, 08, 17),
            };
        }

        [TestMethod]
        public void AllWeekdays() => TestCase(new AllWeekdaysCase());
        private class AllWeekdaysCase : ExpressionCase
        {
            public override string Expression => "* * * * Mon-Fri";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 05, 01),
                new DateTime(2017, 05, 02),
                new DateTime(2017, 05, 03),
                new DateTime(2017, 05, 04),
                new DateTime(2017, 05, 05),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 04, 30),
                new DateTime(2017, 05, 06),
            };
        }

        [TestMethod]
        public void AllWeekends() => TestCase(new AllWeekendsCase());
        private class AllWeekendsCase : ExpressionCase
        {
            public override string Expression => "* * * * 0,6";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 02),
                new DateTime(2017, 09, 03),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01),
                new DateTime(2017, 09, 04),
            };
        }

        #endregion

        #region Hour

        [TestMethod]
        public void BusinessHours() => TestCase(new BusinessHoursCase());
        private class BusinessHoursCase : ExpressionCase
        {
            public override string Expression => "* 9-17 * * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 09, 00, 00),
                new DateTime(2017, 09, 01, 09, 30, 00),
                new DateTime(2017, 09, 01, 10, 00, 00),
                new DateTime(2017, 09, 01, 10, 30, 00),
                new DateTime(2017, 09, 01, 11, 00, 00),
                new DateTime(2017, 09, 01, 11, 30, 00),
                new DateTime(2017, 09, 01, 12, 00, 00),
                new DateTime(2017, 09, 01, 12, 30, 00),
                new DateTime(2017, 09, 01, 13, 00, 00),
                new DateTime(2017, 09, 01, 16, 30, 00),
                new DateTime(2017, 09, 01, 17, 00, 00),
                new DateTime(2017, 09, 01, 17, 30, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 08, 00, 00),
                new DateTime(2017, 09, 01, 08, 30, 00),
                new DateTime(2017, 09, 01, 18, 00, 00),
            };
        }

        [TestMethod]
        public void EverySixHours() => TestCase(new EverySixHoursCase());
        private class EverySixHoursCase : ExpressionCase
        {
            public override string Expression => "* */6 * * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 06, 00, 00),
                new DateTime(2017, 09, 01, 12, 00, 00),
                new DateTime(2017, 09, 01, 00, 00, 00),
                new DateTime(2017, 09, 01, 18, 00, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 01, 00, 00),
                new DateTime(2017, 09, 01, 02, 00, 00),
                new DateTime(2017, 09, 01, 03, 00, 00),
                new DateTime(2017, 09, 01, 04, 00, 00),
                new DateTime(2017, 09, 01, 05, 00, 00),
                new DateTime(2017, 09, 01, 07, 00, 00),
                new DateTime(2017, 09, 01, 08, 00, 00),
                new DateTime(2017, 09, 01, 09, 00, 00),
                new DateTime(2017, 09, 01, 10, 00, 00),
                new DateTime(2017, 09, 01, 11, 00, 00),
                new DateTime(2017, 09, 01, 13, 00, 00),
                new DateTime(2017, 09, 01, 14, 00, 00),
                new DateTime(2017, 09, 01, 15, 00, 00),
                new DateTime(2017, 09, 01, 16, 00, 00),
                new DateTime(2017, 09, 01, 17, 00, 00),
                new DateTime(2017, 09, 01, 19, 00, 00),
                new DateTime(2017, 09, 01, 20, 00, 00),
                new DateTime(2017, 09, 01, 21, 00, 00),
                new DateTime(2017, 09, 01, 22, 00, 00),
                new DateTime(2017, 09, 01, 23, 00, 00),
            };
        }

        #endregion

        #region Minute

        [TestMethod]
        public void BackHalfHour() => TestCase(new BackHalfHourCase());
        private class BackHalfHourCase : ExpressionCase
        {
            public override string Expression => "30-55 * * * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 12, 30, 00),
                new DateTime(2017, 09, 01, 12, 31, 00),
                new DateTime(2017, 09, 01, 12, 54, 00),
                new DateTime(2017, 09, 01, 12, 55, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 12, 29, 00),
                new DateTime(2017, 09, 01, 12, 29, 59),
                new DateTime(2017, 09, 01, 12, 56, 00),
            };
        }

        [TestMethod]
        public void EverySixMinutes() => TestCase(new EverySixMinutesCase());
        private class EverySixMinutesCase : ExpressionCase
        {
            public override string Expression => "*/6 * * * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 00, 00, 00),
                new DateTime(2017, 09, 01, 00, 30, 00),
                new DateTime(2017, 09, 01, 00, 36, 00),
                new DateTime(2017, 09, 01, 00, 42, 00),
                new DateTime(2017, 09, 01, 00, 48, 00),
                new DateTime(2017, 09, 01, 00, 54, 00),
                new DateTime(2017, 09, 01, 01, 00, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 00, 05, 00),
                new DateTime(2017, 09, 01, 00, 10, 00),
                new DateTime(2017, 09, 01, 00, 15, 00),
                new DateTime(2017, 09, 01, 00, 20, 00),
                new DateTime(2017, 09, 01, 00, 25, 00),
            };
        }

        #endregion

        #region Month

        [TestMethod]
        public void Spring() => TestCase(new SpringCase());
        private class SpringCase : ExpressionCase
        {
            public override string Expression => "* * * 03-May *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 03, 01),
                new DateTime(2017, 03, 31),
                new DateTime(2017, 04, 01),
                new DateTime(2017, 04, 30),
                new DateTime(2017, 05, 01),
                new DateTime(2017, 05, 30),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 02, 01),
                new DateTime(2017, 02, 28),
                new DateTime(2017, 06, 01),
            };
        }

        [TestMethod]
        public void Summer() => TestCase(new SummerCase());
        private class SummerCase : ExpressionCase
        {
            public override string Expression => "* * * Jun-Aug *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 06, 01),
                new DateTime(2017, 06, 30),
                new DateTime(2017, 07, 01),
                new DateTime(2017, 07, 31),
                new DateTime(2017, 08, 01),
                new DateTime(2017, 08, 31),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 05, 01),
                new DateTime(2017, 05, 31),
                new DateTime(2017, 09, 01),
            };
        }

        [TestMethod]
        public void Winter() => TestCase(new WinterCase());
        private class WinterCase : ExpressionCase
        {
            public override string Expression => "* * * 12,1-2 *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01),
                new DateTime(2017, 01, 31),
                new DateTime(2017, 02, 01),
                new DateTime(2017, 02, 28),
                new DateTime(2017, 12, 01),
                new DateTime(2017, 12, 31),
                new DateTime(2018, 01, 01),
                new DateTime(2018, 01, 31),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 30),
                new DateTime(2017, 11, 30),
                new DateTime(2018, 03, 01),
            };
        }

        #endregion

        #region Full Expressions

        [TestMethod]
        public void BigExpression() => TestCase(new BigExpressionCase());
        private class BigExpressionCase : ExpressionCase
        {
            public override string Expression => "0,15,30,45 0,6,12,18 * 3,MAY-7,SEP 1-WED,FRI-SAT";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 09, 01, 00, 45, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 04, 01, 00, 00, 00),
                new DateTime(2017, 03, 30, 00, 00, 00),
                new DateTime(2017, 08, 01, 00, 00, 00),
                new DateTime(2017, 03, 27, 01, 45, 00),
                new DateTime(2017, 03, 27, 00, 46, 00),
            };
        }

        [TestMethod]
        public void EveryHourLastFriday() => TestCase(new EveryHourLastFridayCase());
        private class EveryHourLastFridayCase : ExpressionCase
        {
            public override string Expression => "0 * * * 5L";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 03, 31, 00, 00, 00),
                new DateTime(2017, 03, 31, 12, 00, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 03, 31, 00, 30, 00),
                new DateTime(2017, 03, 31, 12, 30, 00),
                new DateTime(2017, 03, 24, 00, 00, 00),
                new DateTime(2017, 03, 24, 00, 30, 00),
                new DateTime(2017, 03, 24, 12, 00, 00),
                new DateTime(2017, 03, 24, 12, 30, 00),
            };
        }

        [TestMethod]
        public void EveryMinuteAtTwo() => TestCase(new EveryMinuteAtTwoCase());
        private class EveryMinuteAtTwoCase : ExpressionCase
        {
            public override string Expression => " * 14 * * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01, 14, 00, 00),
                new DateTime(2017, 01, 01, 14, 25, 00),
                new DateTime(2017, 01, 01, 14, 45, 00),
                new DateTime(2017, 01, 01, 14, 59, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01, 13, 59, 00),
                new DateTime(2017, 01, 01, 15, 00, 00),
            };
        }

        [TestMethod]
        public void MinuteStepAtTwo() => TestCase(new MinuteStepAtTwoCase());
        private class MinuteStepAtTwoCase : ExpressionCase
        {
            public override string Expression => "*/5 14 * * *";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01, 14, 00, 00),
                new DateTime(2017, 01, 01, 14, 05, 00),
                new DateTime(2017, 01, 01, 14, 10, 00),
                new DateTime(2017, 01, 01, 14, 15, 00),
                new DateTime(2017, 01, 01, 14, 20, 00),
                new DateTime(2017, 01, 01, 14, 25, 00),
                new DateTime(2017, 01, 01, 14, 30, 00),
                new DateTime(2017, 01, 01, 14, 35, 00),
                new DateTime(2017, 01, 01, 14, 40, 00),
                new DateTime(2017, 01, 01, 14, 45, 00),
                new DateTime(2017, 01, 01, 14, 50, 00),
                new DateTime(2017, 01, 01, 14, 55, 00),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01, 13, 55, 00),
                new DateTime(2017, 01, 01, 14, 21, 00),
                new DateTime(2017, 01, 01, 14, 44, 00),
                new DateTime(2017, 01, 01, 15, 00, 00),
            };
        }

        [TestMethod]
        public void TwelveNoon() => TestCase(new TwelveNoonCase());
        private class TwelveNoonCase : ExpressionCase
        {
            public override string Expression => "0 12 * * * ";
            public override IEnumerable<DateTime> TrueTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01, 12, 00, 00),
                new DateTime(2017, 01, 01, 12, 00, 59),
            };
            public override IEnumerable<DateTime> FalseTimes => new List<DateTime>
            {
                new DateTime(2017, 01, 01, 11, 59, 00),
                new DateTime(2017, 01, 01, 11, 59, 59),
                new DateTime(2017, 01, 01, 12, 01, 00),
                new DateTime(2017, 01, 01, 12, 01, 59),
            };
        }

        #endregion
    }
}
