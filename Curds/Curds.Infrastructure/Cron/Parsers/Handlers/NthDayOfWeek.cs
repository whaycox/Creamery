using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Curds.Infrastructure.Cron.Ranges;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class NthDayOfWeek : DayOfWeek
    {
        private static readonly Regex NthDayOfWeekMatcher = new Regex($"^({AcceptableCharacterClass}+)#([1-5])$", RegexOptions.Compiled);

        public NthDayOfWeek(ParsingHandler successor)
            : base(successor)
        { }

        public override Ranges.Basic HandleParse(string range, Tokens.Basic token)
        {
            Match nthMatch = NthDayOfWeekMatcher.Match(range);
            if (nthMatch.Success)
                return ParseNthDayOfWeek(nthMatch);
            else
                return Successor.HandleParse(range, token);
        }
        
        private Ranges.NthDayOfWeek ParseNthDayOfWeek(Match nthMatch)
        {
            int dayOfWeek = Translate(nthMatch.Groups[1].Value);
            int nthValue = Translate(nthMatch.Groups[2].Value);
            return new Ranges.NthDayOfWeek(dayOfWeek, nthValue);
        }
    }
}
