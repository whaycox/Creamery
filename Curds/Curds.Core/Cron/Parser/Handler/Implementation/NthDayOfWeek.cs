using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    internal class NthDayOfWeek : DayOfWeek
    {
        private static readonly Regex NthDayOfWeekMatcher = new Regex($"^({AcceptableCharacterClass}+)#([1-5])$", RegexOptions.Compiled);

        public NthDayOfWeek(ParsingHandler successor)
            : base(successor)
        { }

        public override Range.Domain.Basic HandleParse(string range, Token.Domain.Basic token)
        {
            Match nthMatch = NthDayOfWeekMatcher.Match(range);
            if (nthMatch.Success)
                return ParseNthDayOfWeek(nthMatch);
            else
                return Successor.HandleParse(range, token);
        }

        private Range.Implementation.NthDayOfWeek ParseNthDayOfWeek(Match nthMatch)
        {
            int dayOfWeek = Translate(nthMatch.Groups[1].Value);
            int nthValue = Translate(nthMatch.Groups[2].Value);
            return new Range.Implementation.NthDayOfWeek(dayOfWeek, nthValue);
        }
    }
}
