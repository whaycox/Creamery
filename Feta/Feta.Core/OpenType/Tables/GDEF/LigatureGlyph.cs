using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF
{
    using OpenType.Domain;

    public class LigatureGlyph : BaseTable
    {
        public ushort CaretCount { get; set; }
        public ushort[] CaretValueOffsets { get; set; }

        public List<CaretValue> CaretValues { get; set; } = new List<CaretValue>();
    }
}
