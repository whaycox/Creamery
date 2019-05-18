using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;
    using Range.Domain;
    using Range.Implementation;

    public class Wildcard : ParsingHandler
    {
        private static readonly Regex WildcardFormat = new Regex(@"^\*(?:/(\d+))?$", RegexOptions.Compiled);

        public Wildcard(ParsingHandler successor)
            : base(successor)
        { }

        protected override Basic HandleParseInternal(string range)
        {
            Match wildcardMatch = WildcardFormat.Match(range);
            if (wildcardMatch.Success)
                return ParseWildcard(wildcardMatch);
            else
                return Successor.HandleParse(range);
        }

        private Basic ParseWildcard(Match wildcardMatch)
        {
            string stepValue = wildcardMatch.Groups[1].Value;
            if (string.IsNullOrEmpty(stepValue))
                return new Unbounded();
            else
                return new Step(int.Parse(stepValue));
        }
    }
}
