using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    public class WeekdayNearest : ParsingHandler
    {
        private static readonly Regex NearestWeekdayMatcher = new Regex(@"^(\d+)W$", RegexOptions.Compiled);

        public WeekdayNearest(ParsingHandler successor)
            : base(successor)
        { }

        protected override Range.Domain.Basic HandleParseInternal(string range)
        {
            Match nearestWeekdayMatch = NearestWeekdayMatcher.Match(range);
            if (nearestWeekdayMatch.Success)
                return ParseNearestWeekday(nearestWeekdayMatch);
            else
                return Successor.HandleParse(range);
        }

        private Range.Implementation.WeekdayNearest ParseNearestWeekday(Match nearestWeekdayMatch)
        {
            string weekdayNearestTo = nearestWeekdayMatch.Groups[1].Value;
            return new Range.Implementation.WeekdayNearest(ParseRangeComponent(weekdayNearestTo));
        }
    }
}
