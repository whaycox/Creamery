using System;
using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.Offset
{
    using Exceptions;
    using OpenType.Abstraction;
    using Tables.Abstraction;
    using OpenType.Domain;

    public class Persistor : ITablePersistor<Table>
    {
        private static StringComparer TagComparer = StringComparer.Ordinal;

        private IPersistorCollection Persistors { get; }

        public Persistor(IPersistorCollection persistors)
        {
            Persistors = persistors;
        }

        public void Read(IFontReader reader)
        {
            Table table = new Table();
            table.SfntVersion = reader.ReadUInt32();
            table.NumberOfTables = reader.ReadUInt16();
            table.SearchRange = reader.ReadUInt16();
            table.EntrySelector = reader.ReadUInt16();
            table.RangeShift = reader.ReadUInt16();

            for (int i = 0; i < table.NumberOfTables; i++)
            {
                TableRecord tableRecord = ReadRecord(reader);
                ValidateRecord(table, tableRecord);
                table.Records.Add(tableRecord);
            }

            reader.Tables.Add(table);
        }
        private TableRecord ReadRecord(IFontReader reader)
        {
            TableRecord toReturn = new TableRecord();
            toReturn.Tag = reader.ReadTag();
            toReturn.Checksum = reader.ReadUInt32();
            toReturn.Offset = reader.ReadUInt32();
            toReturn.Length = reader.ReadUInt32();
            reader.Offsets.RegisterParser(toReturn.Offset, Persistors.RetrieveParser(toReturn.Tag));

            return toReturn;
        }
        private void ValidateRecord(Table table, TableRecord record)
        {
            if (table.Records.Count == 0)
                return;

            TableRecord lastRecord = table.Records.Last();
            if (TagComparer.Compare(lastRecord.Tag, record.Tag) > 0)
            {
                List<string> baseTags = new List<string>() { lastRecord.Tag, record.Tag };
                var ascending = baseTags.OrderBy(t => t);
                var descending = baseTags.OrderByDescending(t => t);


                throw new MisorderedTagsException();
            }
        }

        public void Write(IFontWriter writer, Table table)
        {
            writer.WriteUInt32(table.SfntVersion);
            writer.WriteUInt16(table.NumberOfTables);
            writer.WriteUInt16(table.SearchRange);
            writer.WriteUInt16(table.EntrySelector);
            writer.WriteUInt16(table.RangeShift);

            foreach (TableRecord record in table.Records)
                WriteRecord(writer, record);
        }
        private void WriteRecord(IFontWriter writer, TableRecord record)
        {
            writer.WriteTag(record.Tag);
            writer.WriteUInt32(record.Checksum);
            writer.DeferWriteUInt32(CalculateOffset(record.Tag));
            writer.DeferWriteUInt32(CalculateLength(record.Tag));
        }
        private DeferredValueSelector<uint> CalculateOffset(string tag)
        {
            DeferredValueSelector<uint> offsetDelegate =
                (ITableCollection tableCollection, IOffsetRegistry offsetRegistry) =>
                {
                    PrimaryTable table = tableCollection.Retrieve(tag);
                    return offsetRegistry.RetrieveOffset(table);
                };
            return offsetDelegate;
        }
        private DeferredValueSelector<uint> CalculateLength(string tag)
        {
            DeferredValueSelector<uint> lengthDelegate =
                (ITableCollection tableCollection, IOffsetRegistry offsetRegistry) =>
                {
                    PrimaryTable table = tableCollection.Retrieve(tag);
                    return offsetRegistry.RetrieveLength(table);
                };
            return lengthDelegate;
        }
    }
}
