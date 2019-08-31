using System;

namespace Feta.OpenType.Tables.GDEF
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Implementation;

    public class LigatureGlyphPersistor : SubtablePersistor<Table, LigatureGlyph>
    {
        private CaretValuePersistor CaretValue { get; } = new CaretValuePersistor();

        protected override LigatureGlyph ReadSubtable(IFontReader reader)
        {
            uint startingOffset = reader.CurrentOffset;

            LigatureGlyph ligatureGlyph = new LigatureGlyph();
            ligatureGlyph.CaretCount = reader.ReadUInt16();
            ligatureGlyph.CaretValueOffsets = new ushort[ligatureGlyph.CaretCount];
            for (int i = 0; i < ligatureGlyph.CaretCount; i++)
            {
                ligatureGlyph.CaretValueOffsets[i] = reader.ReadUInt16();
                reader.Offsets.RegisterParser(startingOffset + ligatureGlyph.CaretValueOffsets[i], CaretValue.Read);
            }

            return ligatureGlyph;
        }
        protected override void AttachSubtable(Table parentTable, LigatureGlyph subTable) => parentTable.AddLigatureGlyph(subTable);

        public override void Write(IFontWriter writer, LigatureGlyph table)
        {
            throw new NotImplementedException();
        }
    }
}
