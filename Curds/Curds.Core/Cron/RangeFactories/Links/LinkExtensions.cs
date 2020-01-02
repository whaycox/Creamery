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
            where TFieldDefinition : ICronFieldDefinition => new SingleValueLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddWildcard<TFieldDefinition>(this IRangeFactoryLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new WildcardLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddRangeValue<TFieldDefinition>(this IRangeFactoryLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new RangeValueLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddNthDayOfWeek(this IRangeFactoryLink rangeLink, DayOfWeekFieldDefinition fieldDefinition) =>
            new NthDayOfWeekLink(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddLastDayOfWeek(this IRangeFactoryLink rangeLink, DayOfWeekFieldDefinition fieldDefinition) =>
            new LastDayOfWeekLink(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddNearestWeekday(this IRangeFactoryLink rangeLink, DayOfMonthFieldDefinition fieldDefinition) =>
            new NearestWeekdayLink(fieldDefinition, rangeLink);

        public static IRangeFactoryLink AddLastDayOfMonth(this IRangeFactoryLink rangeLink, DayOfMonthFieldDefinition fieldDefinition) =>
            new LastDayOfMonthLink(fieldDefinition, rangeLink);
    }
}
