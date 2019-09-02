using System;

namespace Feta.OpenType.Tables.Coverage.Implementation
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Domain;
    using OpenType.Implementation;

    public abstract class CoveragePersistor<T> : SubtablePersistor<T, CoverageTable>
        where T : PrimaryTable
    {
        protected override CoverageTable ReadSubtable(IFontReader reader)
        {
            CoverageTable coverage = new CoverageTable();
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
        private CoverageTable ReadFormatOne(IFontReader reader, CoverageTable coverage)
        {
            coverage.GlyphCount = reader.ReadUInt16();
            coverage.GlyphArray = new ushort[coverage.GlyphCount];
            for (int i = 0; i < coverage.GlyphCount; i++)
                coverage.GlyphArray[i] = reader.ReadUInt16();
            return coverage;
        }
        private CoverageTable ReadFormatTwo(IFontReader reader, CoverageTable coverage)
        {
            coverage.RangeCount = reader.ReadUInt16();
            coverage.RangeRecords = new RangeRecord[coverage.RangeCount];
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

        protected override void Write(IFontWriter writer, CoverageTable table)
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
        private void WriteFormatOne(IFontWriter writer, CoverageTable table)
        {
            writer.WriteUInt16(table.GlyphCount);
            for (int i = 0; i < table.GlyphCount; i++)
                writer.WriteUInt16(table.GlyphArray[i]);
        }
        private void WriteFormatTwo(IFontWriter writer, CoverageTable table)
        {
            writer.WriteUInt16(table.RangeCount);
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
