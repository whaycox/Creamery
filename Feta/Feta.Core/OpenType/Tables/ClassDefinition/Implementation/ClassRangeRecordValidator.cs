using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Feta.OpenType.Tables.ClassDefinition.Implementation
{
    using Domain;
    using OpenType.Domain;

    public class ClassRangeRecordValidator : RangeValidator<ClassRangeRecord>
    {
        protected override ushort LowerBound(ClassRangeRecord range) => range.StartGlyphID;
        protected override ushort UpperBound(ClassRangeRecord range) => range.EndGlyphID;
    }
}
