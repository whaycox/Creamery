using System;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;

    internal class RangeValueRangeLink<TFieldDefinition> : BaseRangeLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        private static readonly Regex RangeMatcher = new Regex(@"^([a-zA-Z0-9]{1,3})-([a-zA-Z0-9]{1,3})$", RegexOptions.Compiled);

        public RangeValueRangeLink(TFieldDefinition fieldDefinition, ICronRangeLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = RangeMatcher.Match(range);
            if (!match.Success)
                return null;

            int low = ParseCapturedValue(match.Groups[1].Value);
            int high = ParseCapturedValue(match.Groups[2].Value);
            if (low > high)
                throw new FormatException($"Cannot supply an inverted range {range}");

            return new RangeValueRange<TFieldDefinition>(FieldDefinition, low, high);
        }
        private int ParseCapturedValue(string captured)
        {
            captured = FieldDefinition.LookupAlias(captured);
            int parsedValue = int.Parse(captured);
            if (!IsValid(parsedValue))
                throw new FormatException($"Supplied value {captured} is outside the allowed {FieldDefinition.AbsoluteMin}-{FieldDefinition.AbsoluteMax}");
            return parsedValue;
        }
    }
}
