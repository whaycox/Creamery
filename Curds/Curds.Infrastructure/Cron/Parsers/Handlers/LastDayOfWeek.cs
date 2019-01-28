using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Curds.Infrastructure.Cron.Ranges;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class LastDayOfWeek : DayOfWeek
    {
        private static readonly Regex LastWeekdayMatcher = new Regex($@"^({AcceptableCharacterClass}+)L$", RegexOptions.Compiled);

        public LastDayOfWeek(ParsingHandler successor)
            : base(successor)
        { }

        public override Ranges.Basic HandleParse(string range, Tokens.Basic token)
        {
            Match lastMatch = LastWeekdayMatcher.Match(range);
            if (lastMatch.Success)
                return ParseLastDayOfWeek(lastMatch);
            else
                return Successor.HandleParse(range, token);
        }

        private Ranges.LastDayOfWeek ParseLastDayOfWeek(Match lastMatch)
        {
            int dayOfWeek = Translate(lastMatch.Groups[1].Value);
            return new Ranges.LastDayOfWeek(dayOfWeek);
        }
    }
}
