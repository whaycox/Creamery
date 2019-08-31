using System;

namespace Feta.OpenType.Tables.Coverage
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Domain;

    public abstract class Persistor<T> : SubtablePersistor<T, Table>
        where T : PrimaryTable
    {
        protected override Table ReadSubtable(IFontReader reader)
        {
            Table coverage = new Table();
            coverage.Format = reader.ReadUInt16();

            switch (coverage.Format)
            {
                case 1:
                    return ReadFormatOne(reader, coverage);
                case 2:
                    return ReadFormatTwo(reader, coverage);
                default:
                    throw new InvalidOperationException($"Unsupported Coverage Format: {coverage.Format}");
            }
        }
        private Table ReadFormatOne(IFontReader reader, Table coverage)
        {
            coverage.GlyphCount = reader.ReadUInt16();
            coverage.GlyphArray = new ushort[coverage.GlyphCount.Value];
            for (int i = 0; i < coverage.GlyphCount; i++)
                coverage.GlyphArray[i] = reader.ReadUInt16();
            return coverage;
        }
        private Table ReadFormatTwo(IFontReader reader, Table coverage)
        {
            coverage.RangeCount = reader.ReadUInt16();
            coverage.RangeRecords = new RangeRecord[coverage.RangeCount.Value];
            for (int i = 0; i < coverage.RangeCount; i++)
                coverage.RangeRecords[i] = ReadRangeRecord(reader);

            return coverage;
        }
        private RangeRecord ReadRangeRecord(IFontReader reader)
        {
            RangeRecord toReturn = new RangeRecord();
            toReturn.StartGlyphID = reader.ReadUInt16();
            toReturn.EndGlyphID = reader.ReadUInt16();
            toReturn.StartCoverageIndex = reader.ReadUInt16();

            return toReturn;
        }

        public override void Write(IFontWriter writer, Table table)
        {
            writer.WriteUInt16(table.Format);
            switch (table.Format)
            {
                case 1:
                    WriteFormatOne(writer, table);
                    break;
                case 2:
                    WriteFormatTwo(writer, table);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported format: {table.Format}");
            }
        }
        private void WriteFormatOne(IFontWriter writer, Table table)
        {
            writer.WriteUInt16(table.GlyphCount.Value);
            for (int i = 0; i < table.GlyphCount; i++)
                writer.WriteUInt16(table.GlyphArray[i]);
        }
        private void WriteFormatTwo(IFontWriter writer, Table table)
        {
            writer.WriteUInt16(table.RangeCount.Value);
            for (int i = 0; i < table.RangeCount; i++)
                WriteRangeRecord(writer, table.RangeRecords[i]);
        }
        private void WriteRangeRecord(IFontWriter writer, RangeRecord rangeRecord)
        {
            writer.WriteUInt16(rangeRecord.StartGlyphID);
            writer.WriteUInt16(rangeRecord.EndGlyphID);
            writer.WriteUInt16(rangeRecord.StartCoverageIndex);
        }
    }
}
