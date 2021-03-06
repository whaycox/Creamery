﻿using System;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;
    using Abstraction;

    internal class NthDayOfWeekLink : BaseLink<DayOfWeekFieldDefinition>
    {
        private static readonly Regex NthDayOfWeekMatcher = new Regex("^([a-zA-Z0-6]{1,3})#([1-5])$", RegexOptions.Compiled);

        public NthDayOfWeekLink(DayOfWeekFieldDefinition fieldDefinition, IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            Match match = NthDayOfWeekMatcher.Match(range);
            if (!match.Success)
                return null;

            int nthValue = int.Parse(match.Groups[2].Value);
            return new NthDayOfWeekRange(FieldDefinition, FieldDefinition.Parse(match.Groups[1].Value), nthValue);
        }
    }
}
