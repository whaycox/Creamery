using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;

    internal class SingleValueRangeLink<TFieldDefinition> : BaseRangeLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public SingleValueRangeLink(
            TFieldDefinition fieldDefinition,
            ICronRangeLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range)
        {
            if (!int.TryParse(range, out int value))
                return null;
            if (!IsValid(value))
                throw new FormatException($"Supplied value {value} is outside the accepted {FieldDefinition.AbsoluteMin}-{FieldDefinition.AbsoluteMax}");
            return new SingleValueRange<TFieldDefinition>(FieldDefinition, value);
        }
        private bool IsValid(int parsedValue) => FieldDefinition.AbsoluteMin <= parsedValue && parsedValue <= FieldDefinition.AbsoluteMax;

    }
}
