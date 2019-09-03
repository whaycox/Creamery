using System;
using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.Offset.Implementation
{
    using Domain;
    using Exceptions;
    using OpenType.Abstraction;
    using OpenType.Domain;
    using OpenType.Implementation;

    public class OffsetPersistor : BaseTablePersistor<OffsetTable>
    {
        private static StringComparer TagComparer = StringComparer.Ordinal;

        private IPersistorCollection Persistors { get; }

        public OffsetPersistor(IPersistorCollection persistors)
        {
            Persistors = persistors;
        }

        public override void Read(IFontReader reader)
        {
            OffsetTable table = new OffsetTable();
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

            ITablePersistor persistor = Persistors.RetrievePersistor(toReturn.Tag);
            reader.Offsets.RegisterParser(toReturn.Offset, persistor.Read);

            return toReturn;
        }
        private void ValidateRecord(OffsetTable table, TableRecord record)
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

        protected override void Write(IFontWriter writer, OffsetTable table)
        {
            if (table.NumberOfTables != table.Records.Count)
                throw new FormatException($"{nameof(table.NumberOfTables)} must match the number of {nameof(TableRecord)}s");

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
