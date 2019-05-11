using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    internal class WeekdayNearest : ParsingHandler
    {
        private static readonly Regex NearestWeekdayMatcher = new Regex(@"^(\d+)W$", RegexOptions.Compiled);

        public WeekdayNearest(ParsingHandler successor)
            : base(successor)
        { }

        public override Range.Domain.Basic HandleParse(string range, Token.Domain.Basic token)
        {
            Match nearestWeekdayMatch = NearestWeekdayMatcher.Match(range);
            if (nearestWeekdayMatch.Success)
                return ParseNearestWeekday(nearestWeekdayMatch);
            else
                return Successor.HandleParse(range, token);
        }

        private Range.Implementation.WeekdayNearest ParseNearestWeekday(Match nearestWeekdayMatch)
        {
            string weekdayNearestTo = nearestWeekdayMatch.Groups[1].Value;
            return new Range.Implementation.WeekdayNearest(int.Parse(weekdayNearestTo));
        }
    }
}
