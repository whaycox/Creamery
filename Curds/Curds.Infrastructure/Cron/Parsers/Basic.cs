using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron.Parsers
{
    using Handlers;

    internal class Basic
    {
        private static readonly char[] Separator = new char[] { ',' };

        public static ParsingHandler EmptyChain => null;

        public IEnumerable<Ranges.Basic> ParseRanges(string ranges, Tokens.Basic token)
        {
            string[] rangeParts = ranges.Trim().Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (rangeParts.Length == 0)
                throw new FormatException("Cannot supply an empty range");

            ParsingHandler chain = Chain;
            foreach (string rangePart in rangeParts)
                yield return chain.HandleParse(rangePart, token);
        }
        protected virtual ParsingHandler Chain =>
            EmptyChain
            .AddWildcard()
            .AddDefinite();
    }
}
