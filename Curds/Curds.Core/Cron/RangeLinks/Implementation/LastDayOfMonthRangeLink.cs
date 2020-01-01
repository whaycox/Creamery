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
        private static readonly Regex LastDayOfMonthMatcher = new Regex(@"^[Ll](?:-(\d+))?$", RegexOptions.Compiled);

        public LastDayOfMonthRangeLink(DayOfMonthFieldDefinition fieldDefinition, ICronRangeLink successor)
            : base(fieldDefinition, successor)
        {        }

        public override ICronRange HandleParse(string range)
        {
            Match match = LastDayOfMonthMatcher.Match(range);
            if (!match.Success)
                return null;

            int offset = default;
            if (match.Groups[1].Success)
                offset = int.Parse(match.Groups[1].Value);

            if (!IsOffsetValid(offset))
                throw new FormatException($"Must supply an Last Day of Month offset less than {FieldDefinition.AbsoluteMax}");

            return new LastDayOfMonthRange(FieldDefinition, offset);
        }
        private bool IsOffsetValid(int offset) => FieldDefinition.AbsoluteMax > offset;
    }
}
