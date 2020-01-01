using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.RangeLinkFactories
{
    using Cron.Abstraction;
    using RangeLinks.Implementation;
    using FieldDefinitions.Implementation;

    internal static class RangeLinkExtensions
    {
        public static ICronRangeLink AddSingleValue<TFieldDefinition>(this ICronRangeLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new SingleValueRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static ICronRangeLink AddWildcard<TFieldDefinition>(this ICronRangeLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new WildcardRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static ICronRangeLink AddRangeValue<TFieldDefinition>(this ICronRangeLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new RangeValueRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);

        public static ICronRangeLink AddNthDayOfWeek(this ICronRangeLink rangeLink, DayOfWeekFieldDefinition fieldDefinition) =>
            new NthDayOfWeekRangeLink(fieldDefinition, rangeLink);

        public static ICronRangeLink AddLastDayOfWeek(this ICronRangeLink rangeLink, DayOfWeekFieldDefinition fieldDefinition) =>
            new LastDayOfWeekRangeLink(fieldDefinition, rangeLink);
    }
}
