using System;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;

    internal class RangeValueRangeLink<TFieldDefinition> : BaseRangeLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        private static readonly Regex RangeMatcher = new Regex(@"^(\d+)-(\d+)$", RegexOptions.Compiled);

        public RangeValueRangeLink(TFieldDefinition fieldDefinition, ICronRangeLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = RangeMatcher.Match(range);
            if (!match.Success)
                return null;

            int low = int.Parse(match.Groups[1].Value);
            if (!IsValid(low))
                throw new FormatException($"Supplied range {range} is lower than allowed minimum {FieldDefinition.AbsoluteMin}");

            int high = int.Parse(match.Groups[2].Value);
            if (!IsValid(high))
                throw new FormatException($"Supplied range {range} is higher than allowed maximum {FieldDefinition.AbsoluteMax}");

            if (low > high)
                throw new FormatException($"Cannot supply an inverted range {range}");

            return new RangeValueRange<TFieldDefinition>(FieldDefinition, low, high);
        }
    }
}
