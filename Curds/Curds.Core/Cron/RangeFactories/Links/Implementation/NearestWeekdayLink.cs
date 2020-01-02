using System;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;
    using Abstraction;

    internal class NearestWeekdayLink : BaseLink<DayOfMonthFieldDefinition>
    {
        private static readonly Regex NearestWeekdayMatcher = new Regex(@"^(\d+)[Ww]$", RegexOptions.Compiled);

        public NearestWeekdayLink(DayOfMonthFieldDefinition fieldDefinition, IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = NearestWeekdayMatcher.Match(range);
            if (!match.Success)
                return null;

            return new NearestWeekdayRange(FieldDefinition, FieldDefinition.Parse(match.Groups[1].Value));
        }
    }
}
