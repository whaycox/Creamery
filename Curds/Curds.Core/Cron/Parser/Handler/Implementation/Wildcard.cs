using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;
    using Range.Implementation;

    internal class Wildcard : ParsingHandler
    {
        private static readonly Regex WildcardFormat = new Regex(@"^\*(?:/(\d+))?$", RegexOptions.Compiled);

        public Wildcard(ParsingHandler successor)
            : base(successor)
        { }

        public override Range.Domain.Basic HandleParse(string range, Token.Domain.Basic token)
        {
            Match wildcardMatch = WildcardFormat.Match(range);
            if (wildcardMatch.Success)
                return ParseWildcard(wildcardMatch, token);
            else
                return Successor.HandleParse(range, token);
        }

        private Range.Domain.Basic ParseWildcard(Match wildcardMatch, Token.Domain.Basic token)
        {
            string stepValue = wildcardMatch.Groups[1].Value;
            if (string.IsNullOrEmpty(stepValue))
                return new Unbounded(token.AbsoluteMin, token.AbsoluteMax);
            else
                return new Step(token.AbsoluteMin, token.AbsoluteMax, int.Parse(stepValue));
        }
    }
}
