using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF
{
    using OpenType.Domain;

    public class LigatureCaretList : BaseTable
    {
        public ushort CoverageOffset { get; set; }
        public ushort LigatureGlyphCount { get; set; }
        public ushort[] LigatureGlyphOffsets { get; set; }

        public List<LigatureGlyph> LigatureGlyphs { get; set; } = new List<LigatureGlyph>();
        public Coverage.Table Coverage { get; set; }
    }
}
