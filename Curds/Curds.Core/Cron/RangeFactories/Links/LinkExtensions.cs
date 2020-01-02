using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.RangeFactories.Links
{
    using Cron.Abstraction;
    using Abstraction;
    using Implementation;
    using FieldDefinitions.Implementation;

    internal static class LinkExtensions
    {
        public static IRangeFactoryLink AddSingleValue<TFieldDefinition>(this IRangeFactoryLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new SingleValueRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddWildcard<TFieldDefinition>(this IRangeFactoryLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new WildcardRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddRangeValue<TFieldDefinition>(this IRangeFactoryLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new RangeValueRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddNthDayOfWeek(this IRangeFactoryLink rangeLink, DayOfWeekFieldDefinition fieldDefinition) =>
            new NthDayOfWeekRangeLink(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddLastDayOfWeek(this IRangeFactoryLink rangeLink, DayOfWeekFieldDefinition fieldDefinition) =>
            new LastDayOfWeekRangeLink(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddNearestWeekday(this IRangeFactoryLink rangeLink, DayOfMonthFieldDefinition fieldDefinition) =>
            new NearestWeekdayRangeLink(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddLastDayOfMonth(this IRangeFactoryLink rangeLink, DayOfMonthFieldDefinition fieldDefinition) =>
            new LastDayOfMonthRangeLink(fieldDefinition, rangeLink);
    }
}
