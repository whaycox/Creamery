using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Curds.Infrastructure.Cron.Ranges;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class WeekdayNearest : ParsingHandler
    {
        private static readonly Regex NearestWeekdayMatcher = new Regex(@"^(\d+)W$", RegexOptions.Compiled);

        public WeekdayNearest(ParsingHandler successor)
            : base(successor)
        { }

        public override Ranges.Basic HandleParse(string range, Tokens.Basic token)
        {
            Match nearestWeekdayMatch = NearestWeekdayMatcher.Match(range);
            if (nearestWeekdayMatch.Success)
                return ParseNearestWeekday(nearestWeekdayMatch);
            else
                return Successor.HandleParse(range, token);
        }

        private NearestWeekday ParseNearestWeekday(Match nearestWeekdayMatch)
        {
            string weekdayNearestTo = nearestWeekdayMatch.Groups[1].Value;
            return new NearestWeekday(int.Parse(weekdayNearestTo));
        }
    }
}
