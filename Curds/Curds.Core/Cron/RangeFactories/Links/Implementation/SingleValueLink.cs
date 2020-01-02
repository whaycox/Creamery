using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;
    using Abstraction;

    internal class SingleValueLink<TFieldDefinition> : BaseLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public SingleValueLink(
            TFieldDefinition fieldDefinition,
            IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            range = FieldDefinition.LookupAlias(range);
            if (!int.TryParse(range, out int value))
                return null;
            if (!IsValid(value))
                throw new FormatException($"Supplied value {value} is outside the accepted {FieldDefinition.AbsoluteMin}-{FieldDefinition.AbsoluteMax}");
            return new SingleValueRange<TFieldDefinition>(FieldDefinition, value);
        }

    }
}
