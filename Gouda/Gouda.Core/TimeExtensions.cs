using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda
{
    using Time.Abstraction;
    using Time.Implementation;

    public static class TimeExtensions
    {
        private static IDateTimeOffsetBuilder CreateBuilder() => new DateTimeOffsetBuilder();

        public static IDateTimeOffsetBuilder WithYear(this DateTimeOffset dummy, int year) => CreateBuilder().WithYear(year);
        public static IDateTimeOffsetBuilder WithYear(this IDateTimeOffsetBuilder builder, int year)
        {
            builder.ApplyYear(year);
            return builder;
        }

        public static IDateTimeOffsetBuilder WithMonth(this DateTimeOffset dummy, int month) => CreateBuilder().WithMonth(month);
        public static IDateTimeOffsetBuilder WithMonth(this IDateTimeOffsetBuilder builder, int month)
        {
            builder.ApplyMonth(month);
            return builder;
        }

        public static IDateTimeOffsetBuilder WithDay(this DateTimeOffset dummy, int day) => CreateBuilder().WithDay(day);
        public static IDateTimeOffsetBuilder WithDay(this IDateTimeOffsetBuilder builder, int day)
        {
            builder.ApplyDay(day);
            return builder;
        }
    }
}
