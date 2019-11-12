using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Time.Implementation
{
    using Abstraction;
    using Domain;

    public class DateTimeOffsetBuilder : IDateTimeOffsetBuilder
    {
        private DateTimeOffsetParts Parts { get; } = new DateTimeOffsetParts();

        public void ApplyYear(int year) => Parts.Year = year;
        public void ApplyMonth(int month) => Parts.Month = month;
        public void ApplyDay(int day) => Parts.Day = day;

        public DateTimeOffset Build() => new DateTimeOffset(
            Parts.Year,
            Parts.Month,
            Parts.Day,
            0,
            0,
            0,
            TimeSpan.Zero);
    }
}
