using System.Text.RegularExpressions;
using System;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class Definite : ParsingHandler
    {
        private static readonly Regex DefiniteFormat = new Regex($"^({AcceptableCharacterClass}+)(?:-({AcceptableCharacterClass}+))?$", RegexOptions.Compiled);

        public Definite(ParsingHandler successor)
            : base(successor)
        { }

        public override Ranges.Basic HandleParse(string range, Tokens.Basic token)
        {
            Match definiteMatch = DefiniteFormat.Match(range);
            if (!definiteMatch.Success)
                return Successor.HandleParse(range, token);
            else
                return ParseDefinite(definiteMatch);
        }

        private Ranges.Basic ParseDefinite(Match definiteMatch)
        {
            var (min, max) = SplitRange(definiteMatch);
            if (string.IsNullOrEmpty(max))
                max = min;

            int parsedMin = Translate(min);
            int parsedMax = Translate(max);
            return new Ranges.Basic(parsedMin, parsedMax);
        }
        protected virtual (string min, string max) SplitRange(Match definiteMatch) => (definiteMatch.Groups[1].Value, definiteMatch.Groups[2].Value);
    }
}
