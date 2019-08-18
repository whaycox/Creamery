using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.Gdef
{
    using OpenType.Domain;

    public class Table : BaseTable
    {
        public const string Tag = "GDEF";

        public ushort MajorVersion { get; set; }
        public ushort MinorVersion { get; set; }
        public ushort GlyphClassDefinitionOffset { get; set; }
        public ushort AttachmentListOffset { get; set; }
        public ushort LigatureCaretListOffset { get; set; }
        public ushort MarkAttachmentClassDefinitionOffset { get; set; }
        public ushort MarkGlyphSetDefinitionOffset { get; set; }
        public uint ItemVariationStoreOffset { get; set; }
    }
}
