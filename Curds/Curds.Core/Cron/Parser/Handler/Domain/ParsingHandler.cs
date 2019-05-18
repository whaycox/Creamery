using System;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Handler.Domain
{
    public abstract class ParsingHandler
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

            return ParseRangeComponent(rangeComponent);
        }
        protected int ParseRangeComponent(string translatedRangeComponent) => int.Parse(translatedRangeComponent);

        public Range.Domain.Basic HandleParse(string range)
        {
            try
            {
                return HandleParseInternal(range);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Failed to parse {range}", ex);
            }
        }
        protected abstract Range.Domain.Basic HandleParseInternal(string range);
    }
}
