using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.RangeLinkFactories
{
    using Cron.Abstraction;
    using RangeLinks.Implementation;

    internal static class RangeLinkExtensions
    {
        public static ICronRangeLink AddSingleValue<TFieldDefinition>(this ICronRangeLink rangeLink, TFieldDefinition fieldDefinition)
            where TFieldDefinition : ICronFieldDefinition => new SingleValueRangeLink<TFieldDefinition>(fieldDefinition, rangeLink);
    }
}
