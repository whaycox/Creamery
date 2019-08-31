using System;

namespace Feta.OpenType.Tables.GDEF
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Implementation;

    public class LigatureCaretListPersistor : SubtablePersistor<Table, LigatureCaretList>
    {
        private LigatureGlyphCoveragePersistor Coverage { get; } = new LigatureGlyphCoveragePersistor();
        private LigatureGlyphPersistor LigatureGlyph { get; } = new LigatureGlyphPersistor();

        protected override LigatureCaretList ReadSubtable(IFontReader reader)
        {
            uint startingOffset = reader.CurrentOffset;

            LigatureCaretList ligatureCaretList = new LigatureCaretList();
            ligatureCaretList.CoverageOffset = reader.ReadUInt16();
            reader.Offsets.RegisterParser(startingOffset + ligatureCaretList.CoverageOffset, Coverage.Read);

            ligatureCaretList.LigatureGlyphCount = reader.ReadUInt16();
            ligatureCaretList.LigatureGlyphOffsets = new ushort[ligatureCaretList.LigatureGlyphCount];
            for (int i = 0; i < ligatureCaretList.LigatureGlyphCount; i++)
            {
                ligatureCaretList.LigatureGlyphOffsets[i] = reader.ReadUInt16();
                reader.Offsets.RegisterParser(startingOffset + ligatureCaretList.LigatureGlyphOffsets[i], LigatureGlyph.Read);
            }

            return ligatureCaretList;
        }
        protected override void AttachSubtable(Table parentTable, LigatureCaretList subTable) => parentTable.LigatureCaretList = subTable;

        public override void Write(IFontWriter writer, LigatureCaretList table)
        {
            throw new NotImplementedException();
        }
    }
}
