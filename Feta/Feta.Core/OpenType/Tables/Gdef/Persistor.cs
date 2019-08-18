using System;
using System.Linq;

namespace Feta.OpenType.Tables.Gdef
{
    using Abstraction;
    using Domain;
    using OpenType.Domain;
    using OpenType.Abstraction;

    public class Persistor : ITablePersistor<Table>
    {
        private delegate Table SubtableParseDelegate(FontReader reader, Table parsedTable);

        public IParsedTables Read(FontReader reader, IParsedTables parsedTables)
        {
            var offsets = parsedTables.Retrieve<Offset.Table>();
            var gdefRecord = offsets.Records.First(r => r.Tag == Table.Tag);
            OffsetTracker offsetTracker = new OffsetTracker(reader);

            Table toReturn = new Table();
            toReturn = ParseHeader(reader, toReturn);

            while (offsetTracker.RelativeOffset < gdefRecord.Length)
                toReturn = ParseSubtable(reader, toReturn, offsetTracker.RelativeOffset);

            throw new NotImplementedException();
        }

        private Table ParseHeader(FontReader reader, Table parsedTable)
        {
            parsedTable.MajorVersion = reader.ReadUInt16();
            parsedTable.MinorVersion = reader.ReadUInt16();
            parsedTable.GlyphClassDefinitionOffset = reader.ReadUInt16();
            parsedTable.AttachmentListOffset = reader.ReadUInt16();
            parsedTable.LigatureCaretListOffset = reader.ReadUInt16();
            parsedTable.MarkAttachmentClassDefinitionOffset = reader.ReadUInt16();

            if (HeaderContainsGlyphSetOffset(parsedTable))
                parsedTable.GlyphClassDefinitionOffset = reader.ReadUInt16();
            if (HeaderContainsItemVariationOffset(parsedTable))
                parsedTable.ItemVariationStoreOffset = reader.ReadUInt32();

            return parsedTable;
        }
        private bool HeaderContainsGlyphSetOffset(Table table) => TableIsVersionOneTwo(table) || TableIsVersionOneThree(table);
        private bool HeaderContainsItemVariationOffset(Table table) => TableIsVersionOneThree(table);
        private bool TableIsVersionOneTwo(Table table) => table.MajorVersion == 1 && table.MinorVersion == 2;
        private bool TableIsVersionOneThree(Table table) => table.MajorVersion == 1 && table.MinorVersion == 3;

        private Table ParseSubtable(FontReader reader, Table parsedTable, int relativeOffset)
        {
            SubtableParseDelegate parseDelegate = IdentifyCurrentSubtable(parsedTable, relativeOffset);
            return parseDelegate(reader, parsedTable);
        }
        private SubtableParseDelegate IdentifyCurrentSubtable(Table parsedTable, int relativeOffset)
        {
            if (parsedTable.GlyphClassDefinitionOffset == relativeOffset)
                return ParseGlyphClassDefinition;
            else
                throw new InvalidOperationException($"No subtable found at relative offset: {relativeOffset}");
        }
        private Table ParseGlyphClassDefinition(FontReader reader, Table parsedTable)
        {
            throw new NotImplementedException();
        }

        public void Write(FontWriter writer, Table table)
        {
            throw new NotImplementedException();
        }
    }
}
