using System;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;

    internal class NearestWeekdayRangeLink : BaseRangeLink<DayOfMonthFieldDefinition>
    {
        private static readonly Regex NearestWeekdayMatcher = new Regex(@"^(\d+)[Ww]$", RegexOptions.Compiled);

        public NearestWeekdayRangeLink(DayOfMonthFieldDefinition fieldDefinition, ICronRangeLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = NearestWeekdayMatcher.Match(range);
            if (!match.Success)
                return null;

            int dayOfMonth = int.Parse(match.Groups[1].Value);
            if (!IsValid(dayOfMonth))
                throw new FormatException($"Cannot supply a day of month {dayOfMonth}");

            return new NearestWeekdayRange(FieldDefinition, dayOfMonth);
        }
    }
}
