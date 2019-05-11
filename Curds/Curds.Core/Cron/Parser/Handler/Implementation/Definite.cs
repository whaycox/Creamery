using System.Text.RegularExpressions;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    internal class Definite : ParsingHandler
    {
        private static readonly Regex DefiniteFormat = new Regex($"^({AcceptableCharacterClass}+)(?:-({AcceptableCharacterClass}+))?$", RegexOptions.Compiled);

        public Definite(ParsingHandler successor)
            : base(successor)
        { }

        public override Range.Domain.Basic HandleParse(string range, Token.Domain.Basic token)
        {
            Match definiteMatch = DefiniteFormat.Match(range);
            if (!definiteMatch.Success)
                return Successor.HandleParse(range, token);
            else
                return ParseDefinite(definiteMatch);
        }

        private Range.Domain.Basic ParseDefinite(Match definiteMatch)
        {
            var (min, max) = SplitRange(definiteMatch);
            if (string.IsNullOrEmpty(max))
                max = min;

            int parsedMin = Translate(min);
            int parsedMax = Translate(max);
            return new Range.Domain.Basic(parsedMin, parsedMax);
        }
        protected virtual (string min, string max) SplitRange(Match definiteMatch) => (definiteMatch.Groups[1].Value, definiteMatch.Groups[2].Value);
    }
}
