using System;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;
    using Abstraction;

    internal class RangeValueLink<TFieldDefinition> : BaseLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        private static readonly Regex RangeMatcher = new Regex(@"^([a-zA-Z0-9]{1,3})-([a-zA-Z0-9]{1,3})$", RegexOptions.Compiled);

        public RangeValueLink(TFieldDefinition fieldDefinition, IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = RangeMatcher.Match(range);
            if (!match.Success)
                return null;

            int low = FieldDefinition.Parse(match.Groups[1].Value);
            int high = FieldDefinition.Parse(match.Groups[2].Value);
            if (low > high)
                throw new FormatException($"Cannot supply an inverted range {range}");

            return new RangeValueRange<TFieldDefinition>(FieldDefinition, low, high);
        }
    }
}
