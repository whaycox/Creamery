using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Feta.OpenType.Domain
{
    using Exceptions;

    public abstract class RangeValidator<T>
    {
        protected List<T> Ranges { get; } = new List<T>();

        protected abstract ushort LowerBound(T range);
        protected abstract ushort UpperBound(T range);

        public void Add(T range)
        {
            if (range == null)
                throw new ArgumentNullException(nameof(range));
            ValidateNewRecord(range);
            Ranges.Add(range);
        }
        protected virtual void ValidateNewRecord(T range)
        {
            ushort lower = LowerBound(range);
            ushort upper = UpperBound(range);

            if (Ranges.Any(r => LowerBound(r) >= lower))
                throw new RangeFormatException($"{nameof(Ranges)} {nameof(LowerBound)} must be in numerical order");

            if (ValueIsInExistingRange(lower))
                throw new RangeFormatException($"{nameof(Ranges)} cannot be overlapping");
            if (ValueIsInExistingRange(upper))
                throw new RangeFormatException($"{nameof(Ranges)} cannot be overlapping");
        }
        private bool ValueIsInExistingRange(ushort id) => Ranges.Any(r => LowerBound(r) <= id && id <= UpperBound(r));


    }
}
