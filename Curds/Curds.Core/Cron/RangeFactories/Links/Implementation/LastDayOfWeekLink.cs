using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;
    using Abstraction;

    internal class LastDayOfWeekLink : BaseLink<DayOfWeekFieldDefinition>
    {
        private static readonly Regex LastDayOfWeekMatcher = new Regex("^([a-zA-Z0-6]{1,3})[lL]$", RegexOptions.Compiled);

        public LastDayOfWeekLink(DayOfWeekFieldDefinition fieldDefinition, IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = LastDayOfWeekMatcher.Match(range);
            if (!match.Success)
                return null;

            return new LastDayOfWeekRange(FieldDefinition, FieldDefinition.Parse(match.Groups[1].Value));
        }
    }
}
