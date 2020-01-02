using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;
    using Abstraction;

    internal class WildcardRangeLink<TFieldDefinition> : BaseRangeLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        private static readonly Regex WildcardMatcher = new Regex(@"^\*(?:/(\d+))?$", RegexOptions.Compiled);

        public WildcardRangeLink(TFieldDefinition fieldDefinition, IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = WildcardMatcher.Match(range);

            if (!match.Success)
                return null;
            else if (!match.Groups[1].Success)
                return new WildcardRange();

            int stepValue = int.Parse(match.Groups[1].Value);
            return new StepRange<TFieldDefinition>(FieldDefinition, stepValue);
        }
    }
}
