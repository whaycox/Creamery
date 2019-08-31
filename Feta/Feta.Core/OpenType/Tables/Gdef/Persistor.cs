using System;
using System.Linq;

namespace Feta.OpenType.Tables.GDEF
{
    using Abstraction;
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class Persistor : PrimaryTablePersistor<Table>
    {
        private GlyphClassDefinitionPersistor GlyphClassDefinition { get; } = new GlyphClassDefinitionPersistor();
        private LigatureCaretListPersistor LigatureCaretList { get; } = new LigatureCaretListPersistor();
        private MarkAttachmentClassDefinitionPersistor MarkAttachmentClassDefinition { get; } = new MarkAttachmentClassDefinitionPersistor();

        protected override Table ReadTable(uint startingOffset, IFontReader reader)
        {
            Table toReturn = new Table();

            toReturn.MajorVersion = reader.ReadUInt16();
            toReturn.MinorVersion = reader.ReadUInt16();
            toReturn.GlyphClassDefinitionOffset = reader.ReadUInt16();
            toReturn.AttachmentListOffset = reader.ReadUInt16();
            toReturn.LigatureCaretListOffset = reader.ReadUInt16();
            toReturn.MarkAttachmentClassDefinitionOffset = reader.ReadUInt16();

            if (HeaderContainsMarkGlyphSetOffset(toReturn))
                toReturn.MarkGlyphSetDefinitionOffset = reader.ReadUInt16();
            if (HeaderContainsItemVariationOffset(toReturn))
                toReturn.ItemVariationStoreOffset = reader.ReadUInt32();

            RegisterOffsets(startingOffset, reader.Offsets, toReturn);
            return toReturn;
        }
        private bool HeaderContainsMarkGlyphSetOffset(Table table) => TableIsVersionOneTwo(table) || TableIsVersionOneThree(table);
        private bool HeaderContainsItemVariationOffset(Table table) => TableIsVersionOneThree(table);
        private bool TableIsVersionOneTwo(Table table) => table.MajorVersion == 1 && table.MinorVersion == 2;
        private bool TableIsVersionOneThree(Table table) => table.MajorVersion == 1 && table.MinorVersion == 3;

        private void RegisterOffsets(uint startingOffset, IOffsetRegistry offsets, Table toAdd)
        {
            if (toAdd.GlyphClassDefinitionOffset > 0)
                offsets.RegisterParser(startingOffset + toAdd.GlyphClassDefinitionOffset, GlyphClassDefinition.Read);
            if (toAdd.AttachmentListOffset > 0)
                offsets.RegisterParser(startingOffset + toAdd.AttachmentListOffset, ParseAttachmentList);
            if (toAdd.LigatureCaretListOffset > 0)
                offsets.RegisterParser(startingOffset + toAdd.LigatureCaretListOffset, LigatureCaretList.Read);
            if (toAdd.MarkAttachmentClassDefinitionOffset > 0)
                offsets.RegisterParser(startingOffset + toAdd.MarkAttachmentClassDefinitionOffset, MarkAttachmentClassDefinition.Read);
            if (toAdd.MarkGlyphSetDefinitionOffset > 0)
                offsets.RegisterParser(startingOffset + toAdd.MarkGlyphSetDefinitionOffset, ParseMarkGlyphSetDefinition);
            if (toAdd.ItemVariationStoreOffset > 0)
                offsets.RegisterParser(startingOffset + toAdd.ItemVariationStoreOffset, ParseItemVariationStore);
        }

        private void ParseAttachmentList(IFontReader reader)
        {
            throw new NotImplementedException();
        }
        private void ParseMarkGlyphSetDefinition(IFontReader reader)
        {
            throw new NotImplementedException();
        }
        private void ParseItemVariationStore(IFontReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Write(IFontWriter writer, Table table)
        {
            throw new NotImplementedException();
        }
    }
}
