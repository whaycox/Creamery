using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;

    internal class LastDayOfWeekRangeLink : BaseRangeLink<DayOfWeekFieldDefinition>
    {
        private static readonly Regex LastDayOfWeekMatcher = new Regex("^([a-zA-Z0-6]{1,3})[lL]$", RegexOptions.Compiled);

        public LastDayOfWeekRangeLink(DayOfWeekFieldDefinition fieldDefinition, ICronRangeLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = LastDayOfWeekMatcher.Match(range);
            if (!match.Success)
                return null;

            string dayOfWeekString = FieldDefinition.LookupAlias(match.Groups[1].Value);
            int dayOfWeek = int.Parse(dayOfWeekString);
            if (!IsValid(dayOfWeek))
                throw new FormatException($"Invalid Day of Week {dayOfWeekString}");

            return new LastDayOfWeekRange(FieldDefinition, dayOfWeek);
        }
    }
}
