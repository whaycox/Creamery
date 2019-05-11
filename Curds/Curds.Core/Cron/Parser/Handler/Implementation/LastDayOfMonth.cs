using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    internal class LastDayOfMonth : ParsingHandler
    {
        private static readonly Regex LastDayOfMonthMatcher = new Regex(@"^L(?:-(\d+))?$", RegexOptions.Compiled);

        public LastDayOfMonth(ParsingHandler successor)
            : base(successor)
        { }

        public override Range.Domain.Basic HandleParse(string range, Token.Domain.Basic token)
        {
            Match lastMatch = LastDayOfMonthMatcher.Match(range);
            if (lastMatch.Success)
                return ParseLastDayOfMonth(lastMatch);
            else
                return Successor.HandleParse(range, token);
        }

        private Range.Implementation.LastDayOfMonth ParseLastDayOfMonth(Match lastMatch)
        {
            int offset = 0;
            string lastOffset = lastMatch.Groups[1].Value;
            if (!string.IsNullOrEmpty(lastOffset))
                offset = int.Parse(lastOffset);

            return new Range.Implementation.LastDayOfMonth(offset);
        }
    }
}
