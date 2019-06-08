using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    using Enumeration;

    [TestClass]
    public class Step : Template.Unbounded<Implementation.Step>
    {
        private Implementation.Step _obj = new Implementation.Step(1);
        protected override Implementation.Step TestObject => _obj;
        protected override ExpressionPart TestingPart => ExpressionPart.Minute;

        [TestMethod]
        public void ThrowsOnInvalidStep()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.Step(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.Step(0));
        }

        [TestMethod]
        public void IsTrueOnlyOnStepIncrements()
        {
            for (int i = 1; i <= 10; i++)
            {
                _obj = new Implementation.Step(i);
                TestMinutesOnStep(i);
            }
        }
        private void TestMinutesOnStep(int step)
        {
            DateTime testTime = new DateTime(2019, 05, 13, 10, 00, 00);
            for (int i = Token.Domain.Minute.MinMinute; i <= Token.Domain.Minute.MaxMinute; i++)
            {
                if (i % step == 0)
                    Assert.IsTrue(TestObject.Test(TestToken, testTime));
                else
                    Assert.IsFalse(TestObject.Test(TestToken, testTime));

                testTime = testTime.AddMinutes(1);
            }
        }

        [TestMethod]
        public void IsTrueWhenMinIsntZero()
        {
            MockToken.SetExpressionPart(ExpressionPart.DayOfMonth);
            for (int i = 1; i <= 10; i++)
            {
                _obj = new Implementation.Step(i);
                TestDaysOfMonthOnStep(i);
            }
        }
        private void TestDaysOfMonthOnStep(int step)
        {
            DateTime testTime = new DateTime(2019, 05, 01);
            var token = BuildTestToken(ExpressionPart.DayOfMonth, TestObject);
            for (int i = Token.Domain.DayOfMonth.MinDayOfMonth; i <= Token.Domain.DayOfMonth.MaxDayOfMonth; i++)
            {
                if ((i - 1) % step == 0)
                    Assert.IsTrue(TestObject.Test(token, testTime));
                else
                    Assert.IsFalse(TestObject.Test(token, testTime));

                testTime = testTime.AddDays(1);
            }
        }
    }
}
