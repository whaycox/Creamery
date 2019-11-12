using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Tests
{
    using Time.Abstraction;

    [TestClass]
    public class TimeExtensionsTest
    {
        private int TestYear = 1234;
        private int TestMonth = 7;
        private int TestDay = 13;

        [TestMethod]
        public void CanBuildWithJustYear()
        {
            DateTimeOffset built = new DateTimeOffset()
                .WithYear(TestYear)
                .Build();

            Assert.AreEqual(TestYear, built.Year);
            Assert.AreEqual(1, built.Month);
            Assert.AreEqual(1, built.Day);
            Assert.AreEqual(default, built.Hour);
            Assert.AreEqual(default, built.Minute);
            Assert.AreEqual(default, built.Second);
            Assert.AreEqual(TimeSpan.Zero, built.Offset);
        }

        [TestMethod]
        public void CanBuildWithJustMonth()
        {
            DateTimeOffset built = new DateTimeOffset()
                .WithMonth(TestMonth)
                .Build();

            Assert.AreEqual(1, built.Year);
            Assert.AreEqual(TestMonth, built.Month);
            Assert.AreEqual(1, built.Day);
            Assert.AreEqual(default, built.Hour);
            Assert.AreEqual(default, built.Minute);
            Assert.AreEqual(default, built.Second);
            Assert.AreEqual(TimeSpan.Zero, built.Offset);
        }

        [TestMethod]
        public void CanBuildWithJustDay()
        {
            DateTimeOffset built = new DateTimeOffset()
                .WithDay(TestDay)
                .Build();

            Assert.AreEqual(1, built.Year);
            Assert.AreEqual(1, built.Month);
            Assert.AreEqual(TestDay, built.Day);
            Assert.AreEqual(default, built.Hour);
            Assert.AreEqual(default, built.Minute);
            Assert.AreEqual(default, built.Second);
            Assert.AreEqual(TimeSpan.Zero, built.Offset);
        }

        private void VerifyOnlyTestDate(DateTimeOffset builtTime)
        {
            Assert.AreEqual(TestYear, builtTime.Year);
            Assert.AreEqual(TestMonth, builtTime.Month);
            Assert.AreEqual(TestDay, builtTime.Day);
            Assert.AreEqual(default, builtTime.Hour);
            Assert.AreEqual(default, builtTime.Minute);
            Assert.AreEqual(default, builtTime.Second);
            Assert.AreEqual(TimeSpan.Zero, builtTime.Offset);
        }

        [DataTestMethod]
        public void CanBuildWithYearMonthDay()
        {
            DateTimeOffset built = new DateTimeOffset()
                .WithYear(TestYear)
                .WithMonth(TestMonth)
                .WithDay(TestDay)
                .Build();

            VerifyOnlyTestDate(built);
        }

        [DataTestMethod]
        public void CanBuildWithMonthDayYear()
        {
            DateTimeOffset built = new DateTimeOffset()
                .WithMonth(TestMonth)
                .WithDay(TestDay)
                .WithYear(TestYear)
                .Build();

            VerifyOnlyTestDate(built);
        }

        [DataTestMethod]
        public void CanBuildWithDayYearMonth()
        {
            DateTimeOffset built = new DateTimeOffset()
                .WithDay(TestDay)
                .WithYear(TestYear)
                .WithMonth(TestMonth)
                .Build();

            VerifyOnlyTestDate(built);
        }
    }
}
