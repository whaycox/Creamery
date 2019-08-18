using System.Linq;

namespace Feta.OpenType.Tables.Offset
{
    using OpenType.Abstraction;
    using OpenType.Domain;
    using Tables.Abstraction;

    public class Persistor : ITablePersistor<Table>
    {
        private IPersistorCollection Persistors { get; }

        public Persistor(IPersistorCollection persistors)
        {
            Persistors = persistors;
        }

        public IParsedTables Read(FontReader reader, IParsedTables parsedTables)
        {
            Table toAdd = new Table();
            toAdd.SfntVersion = reader.ReadSfntVersion();
            toAdd.NumberOfTables = reader.ReadUInt16();
            toAdd.SearchRange = reader.ReadUInt16();
            toAdd.EntrySelector = reader.ReadUInt16();
            toAdd.RangeShift = reader.ReadUInt16();

            for (int i = 0; i < toAdd.NumberOfTables; i++)
                toAdd.Records.Add(ReadRecord(reader, parsedTables));

            parsedTables.Add(toAdd);
            return parsedTables;
        }
        private TableRecord ReadRecord(FontReader reader, IParsedTables parsedTables)
        {
            TableRecord toReturn = new TableRecord();
            toReturn.Tag = reader.ReadTag();
            toReturn.Checksum = reader.ReadUInt32();
            toReturn.Offset = reader.ReadUInt32();
            toReturn.Length = reader.ReadUInt32();
            parsedTables.Register(toReturn.Offset, Persistors.RetrieveParser(toReturn.Tag));

            return toReturn;
        }

        public void Write(FontWriter writer, Table table)
        {
            writer.WriteSfntVersion(table.SfntVersion);
            writer.WriteUInt16(table.NumberOfTables);
            writer.WriteUInt16(table.SearchRange);
            writer.WriteUInt16(table.EntrySelector);
            writer.WriteUInt16(table.RangeShift);

            foreach (TableRecord record in table.Records.OrderBy(r => r.Tag))
                WriteRecord(writer, record);
        }
        private void WriteRecord(FontWriter writer, TableRecord record)
        {
            writer.WriteTag(record.Tag);
            writer.WriteUInt32(record.Checksum);
            writer.WriteUInt32(record.Offset);
            writer.WriteUInt32(record.Length);
        }
    }
}
