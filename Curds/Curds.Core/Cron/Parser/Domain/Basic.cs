using System;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Domain
{
    using Handler.Domain;

    public class Basic
    {
        private static readonly char[] Separator = new char[] { ',' };

        public static ParsingHandler EmptyChain => null;

        public IEnumerable<Range.Domain.Basic> ParseRanges(string ranges)
        {
            string[] rangeParts = ranges.Trim().Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (rangeParts.Length == 0)
                throw new FormatException("Cannot supply an empty range");

            ParsingHandler chain = Chain;
            foreach (string rangePart in rangeParts)
                yield return chain.HandleParse(rangePart);
        }
        protected virtual ParsingHandler Chain => EmptyChain
            .AddWildcard()
            .AddDefinite();
    }
}
