using System.Linq;

namespace Feta.OpenType.Tables.GDEF.Domain
{
    using OpenType.Domain;
    using ClassDefinition.Domain;
    using Coverage.Domain;

    public class GdefTable : PrimaryTable
    {
        public ushort MajorVersion { get; set; }
        public ushort MinorVersion { get; set; }
        public ushort GlyphClassDefinitionOffset { get; set; }
        public ushort AttachmentListOffset { get; set; }
        public ushort LigatureCaretListOffset { get; set; }
        public ushort MarkAttachmentClassDefinitionOffset { get; set; }
        public ushort MarkGlyphSetDefinitionOffset { get; set; }
        public uint ItemVariationStoreOffset { get; set; }

        public ClassDefinitionTable GlyphClassDefinition { get; set; }
        public LigatureCaretList LigatureCaretList { get; set; }
        public ClassDefinitionTable MarkAttachmentClassDefinition { get; set; }

        public GdefTable(string tag)
            : base(tag)
        { }

        public void AddLigatureGlyph(LigatureGlyph ligatureGlyph) => LigatureCaretList
            .LigatureGlyphs
            .Add(ligatureGlyph);
        public void AddCaretValue(CaretValue caretValue) => LigatureCaretList
            .LigatureGlyphs
            .Last()
            .CaretValues
            .Add(caretValue);
        public void AddCoverage(CoverageTable coverage) => LigatureCaretList.Coverage = coverage;
    }
}
