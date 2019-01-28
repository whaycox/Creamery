using System;
using System.Text.RegularExpressions;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class LastDayOfMonth : ParsingHandler
    {
        private static readonly Regex LastDayOfMonthMatcher = new Regex(@"^L(?:-(\d+))?$", RegexOptions.Compiled);

        public LastDayOfMonth(ParsingHandler successor)
            : base(successor)
        { }

        public override Ranges.Basic HandleParse(string range, Tokens.Basic token)
        {
            Match lastMatch = LastDayOfMonthMatcher.Match(range);
            if (lastMatch.Success)
                return ParseLastDayOfMonth(lastMatch);
            else
                return Successor.HandleParse(range, token);
        }

        private Ranges.LastDayOfMonth ParseLastDayOfMonth(Match lastMatch)
        {
            int offset = 0;
            string lastOffset = lastMatch.Groups[1].Value;
            if (!string.IsNullOrEmpty(lastOffset))
                offset = int.Parse(lastOffset);

            return new Ranges.LastDayOfMonth(offset);
        }
    }
}
