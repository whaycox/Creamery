using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;

    internal class LastDayOfMonthRangeLink : BaseRangeLink<DayOfMonthFieldDefinition>
    {
        private static readonly Regex LastDayOfMonthMatcher = new Regex("^[Ll]$", RegexOptions.Compiled);

        public LastDayOfMonthRangeLink(DayOfMonthFieldDefinition fieldDefinition, ICronRangeLink successor)
            : base(fieldDefinition, successor)
        {        }

        public override ICronRange HandleParse(string range)
        {
            Match match = LastDayOfMonthMatcher.Match(range);
            if (!match.Success)
                return null;

            return new LastDayOfMonthRange(FieldDefinition);
        }
    }
}
