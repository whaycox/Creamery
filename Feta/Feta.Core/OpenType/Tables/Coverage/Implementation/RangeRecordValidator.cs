using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Feta.OpenType.Tables.Coverage.Implementation
{
    using OpenType.Domain;
    using Domain;
    using Exceptions;

    public class RangeRecordValidator : RangeValidator<RangeRecord>
    {
        protected override ushort LowerBound(RangeRecord range) => range.StartGlyphID;
        protected override ushort UpperBound(RangeRecord range) => range.EndGlyphID;

        protected override void ValidateNewRecord(RangeRecord range)
        {
            base.ValidateNewRecord(range);
            if (range.StartCoverageIndex != ExpectedStartCoverageIndex())
                throw new RangeFormatException($"{nameof(range.StartCoverageIndex)} values must be contiguous");            
        }
        private ushort ExpectedStartCoverageIndex()
        {
            if (Ranges.Count == 0)
                return 0;
            else
            {
                RangeRecord last = Ranges.Last();
                return (ushort)(last.StartCoverageIndex + last.EndGlyphID - last.StartGlyphID + 1);
            }
        }
    }
}
