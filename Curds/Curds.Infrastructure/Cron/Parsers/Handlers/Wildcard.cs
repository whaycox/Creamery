using Curds.Infrastructure.Cron.Ranges;
using System;
using System.Text.RegularExpressions;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class Wildcard : ParsingHandler
    {
        private static readonly Regex WildcardFormat = new Regex(@"^\*(?:/(\d+))?$", RegexOptions.Compiled);

        public Wildcard(ParsingHandler successor)
            : base(successor)
        { }

        public override Ranges.Basic HandleParse(string range, Tokens.Basic token)
        {
            Match wildcardMatch = WildcardFormat.Match(range);
            if (wildcardMatch.Success)
                return ParseWildcard(wildcardMatch, token);
            else
                return Successor.HandleParse(range, token);
        }

        private Ranges.Basic ParseWildcard(Match wildcardMatch, Tokens.Basic token)
        {
            string stepValue = wildcardMatch.Groups[1].Value;
            if (string.IsNullOrEmpty(stepValue))
                return new Unbounded(token.AbsoluteMin, token.AbsoluteMax);
            else
                return new Step(token.AbsoluteMin, token.AbsoluteMax, int.Parse(stepValue));
        }
    }
}
