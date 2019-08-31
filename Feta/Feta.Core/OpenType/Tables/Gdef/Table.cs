using System.Linq;

namespace Feta.OpenType.Tables.GDEF
{
    using OpenType.Domain;

    public class Table : PrimaryTable
    {
        public const string GdefTag = nameof(GDEF);
        public override string Tag => GdefTag;

        public ushort MajorVersion { get; set; }
        public ushort MinorVersion { get; set; }
        public ushort GlyphClassDefinitionOffset { get; set; }
        public ushort AttachmentListOffset { get; set; }
        public ushort LigatureCaretListOffset { get; set; }
        public ushort MarkAttachmentClassDefinitionOffset { get; set; }
        public ushort MarkGlyphSetDefinitionOffset { get; set; }
        public uint ItemVariationStoreOffset { get; set; }

        public ClassDefinition.Table GlyphClassDefinition { get; set; }
        public LigatureCaretList LigatureCaretList { get; set; }
        public ClassDefinition.Table MarkAttachmentClassDefinition { get; set; }

        public void AddLigatureGlyph(LigatureGlyph ligatureGlyph) => LigatureCaretList
            .LigatureGlyphs
            .Add(ligatureGlyph);
        public void AddCaretValue(CaretValue caretValue) => LigatureCaretList
            .LigatureGlyphs
            .Last()
            .CaretValues
            .Add(caretValue);
        public void AddCoverage(Coverage.Table coverage) => LigatureCaretList.Coverage = coverage;
    }
}
