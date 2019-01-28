using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal abstract class ParsingHandler
    {
        public const string AcceptableCharacterClass = "[A-Za-z0-9]";

        private ParsingHandler _successor = null;
        protected ParsingHandler Successor => _successor ?? throw new FormatException("End of chain reached");

        public ParsingHandler(ParsingHandler successor)
        {
            _successor = successor;
        }

        protected virtual Dictionary<string, int> Lookups => null;
        protected int Translate(string rangeComponent)
        {
            Dictionary<string, int> lookups = Lookups;
            if (lookups != null && lookups.ContainsKey(rangeComponent))
                rangeComponent = lookups[rangeComponent].ToString();

            return int.Parse(rangeComponent);
        }

        public abstract Ranges.Basic HandleParse(string range, Tokens.Basic token);
    }
}
