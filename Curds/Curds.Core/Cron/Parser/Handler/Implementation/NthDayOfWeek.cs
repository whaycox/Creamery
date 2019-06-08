using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    public class NthDayOfWeek : DayOfWeek
    {
        private static readonly Regex NthDayOfWeekMatcher = new Regex($"^({AcceptableCharacterClass}+)#([1-5])$", RegexOptions.Compiled);

        public NthDayOfWeek(ParsingHandler successor)
            : base(successor)
        { }

        protected override Range.Domain.Basic HandleParseInternal(string range)
        {
            Match nthMatch = NthDayOfWeekMatcher.Match(range);
            if (nthMatch.Success)
                return ParseNthDayOfWeek(nthMatch);
            else
                return Successor.HandleParse(range);
        }

        private Range.Implementation.NthDayOfWeek ParseNthDayOfWeek(Match nthMatch)
        {
            int dayOfWeek = Translate(nthMatch.Groups[1].Value);
            int nthValue = ParseRangeComponent(nthMatch.Groups[2].Value);
            return new Range.Implementation.NthDayOfWeek(dayOfWeek, nthValue);
        }
    }
}
