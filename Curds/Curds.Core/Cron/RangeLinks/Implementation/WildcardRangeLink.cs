using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Abstraction;
    using Ranges.Implementation;

    internal class WildcardRangeLink : ICronRangeLink
    {
        private static readonly Regex WildcardMatcher = new Regex(@"^\*$", RegexOptions.Compiled);

        public ICronRangeLink Successor { get; }

        public WildcardRangeLink(ICronRangeLink successor)
        {
            Successor = successor;
        }

        public ICronRange HandleParse(string range)
        {
            if (!WildcardMatcher.IsMatch(range))
                return null;
            return new WildcardRange();
        }
    }
}
