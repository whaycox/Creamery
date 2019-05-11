using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    public class LastDayOfWeek : DayOfWeek
    {
        private static readonly Regex LastWeekdayMatcher = new Regex($@"^({AcceptableCharacterClass}+)L$", RegexOptions.Compiled);

        public LastDayOfWeek(ParsingHandler successor)
            : base(successor)
        { }

        public override Range.Domain.Basic HandleParse(string range)
        {
            Match lastMatch = LastWeekdayMatcher.Match(range);
            if (lastMatch.Success)
                return ParseLastDayOfWeek(lastMatch);
            else
                return Successor.HandleParse(range);
        }

        private Range.Implementation.LastDayOfWeek ParseLastDayOfWeek(Match lastMatch)
        {
            int dayOfWeek = Translate(lastMatch.Groups[1].Value);
            return new Range.Implementation.LastDayOfWeek(dayOfWeek);
        }
    }
}
