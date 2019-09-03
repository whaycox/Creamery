using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.Coverage.Template
{
    using Domain;
    using Implementation;
    using OpenType.Domain;
    using OpenType.Mock;
    using OpenType.Template;

    public abstract class CoveragePersistorTemplate<T> : ITablePersistorTemplate<CoveragePersistor<T>, CoverageTable>
        where T : PrimaryTable
    {
        protected override void PrimeTableToRead(MockFontReader mockReader, CoverageTable table)
        {
            mockReader.PreparedUInt16s.Enqueue(table.Format);

            switch (table.Format)
            {
                case 1:
                    PrimeFormatOne(mockReader, table);
                    break;
                case 2:
                    PrimeFormatTwo(mockReader, table);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
        private void PrimeFormatOne(MockFontReader mockReader, CoverageTable table)
        {
            mockReader.PreparedUInt16s.Enqueue(table.GlyphCount);
            foreach (ushort id in table.GlyphArray)
                mockReader.PreparedUInt16s.Enqueue(id);
        }
        private void PrimeFormatTwo(MockFontReader mockReader, CoverageTable table)
        {
            mockReader.PreparedUInt16s.Enqueue(table.RangeCount);
            foreach (RangeRecord record in table.RangeRecords)
                PrimeRangeRecord(mockReader, record);
        }
        private void PrimeRangeRecord(MockFontReader mockReader, RangeRecord record)
        {
            mockReader.PreparedUInt16s.Enqueue(record.StartGlyphID);
            mockReader.PreparedUInt16s.Enqueue(record.EndGlyphID);
            mockReader.PreparedUInt16s.Enqueue(record.StartCoverageIndex);
        }

        protected override void VerifyTablesAreEqual(CoverageTable expected, CoverageTable actual)
        {
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.GlyphCount, actual.GlyphCount);
            if (expected.GlyphArray != null)
            {
                Assert.AreEqual(expected.GlyphArray.Length, actual.GlyphArray.Length);
                for (int i = 0; i < expected.GlyphArray.Length; i++)
                    Assert.AreEqual(expected.GlyphArray[i], actual.GlyphArray[i]);
            }
            else
                Assert.IsNull(actual.GlyphArray);

            Assert.AreEqual(expected.RangeCount, actual.RangeCount);
            if (expected.RangeRecords != null)
            {
                Assert.AreEqual(expected.RangeRecords.Length, actual.RangeRecords.Length);
                for (int i = 0; i < expected.RangeRecords.Length; i++)
                    VerifyRangeRecordsAreEqual(expected.RangeRecords[i], actual.RangeRecords[i]);
            }
            else
                Assert.IsNull(actual.RangeRecords);
        }
        private void VerifyRangeRecordsAreEqual(RangeRecord expected, RangeRecord actual)
        {
            Assert.AreEqual(expected.StartGlyphID, actual.StartGlyphID);
            Assert.AreEqual(expected.EndGlyphID, actual.EndGlyphID);
            Assert.AreEqual(expected.StartCoverageIndex, actual.StartCoverageIndex);
        }

        protected override void VerifyTableWasWritten(MockFontWriter mockWriter, CoverageTable table)
        {
            Assert.AreEqual(table.Format, mockWriter.WrittenObjects[0]);

            switch (table.Format)
            {
                case 1:
                    VerifyFormatOneWasWritten(mockWriter, table);
                    break;
                case 2:
                    VerifyFormatTwoWasWritten(mockWriter, table);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
        private void VerifyFormatOneWasWritten(MockFontWriter mockWriter, CoverageTable table)
        {
            Assert.AreEqual(table.GlyphCount, mockWriter.WrittenObjects[1]);
            for (int i = 0; i < table.GlyphCount; i++)
                Assert.AreEqual(table.GlyphArray[i], mockWriter.WrittenObjects[i + 2]);
            Assert.AreEqual(table.GlyphCount + 2, mockWriter.WrittenObjects.Count);
        }
        private void VerifyFormatTwoWasWritten(MockFontWriter mockWriter, CoverageTable table)
        {
            Assert.AreEqual(table.RangeCount, mockWriter.WrittenObjects[1]);
            for (int i = 0; i < table.RangeCount; i++)
                VerifyRangeRecordWasWritten(mockWriter, i, table.RangeRecords[i]);
            Assert.AreEqual(table.RangeCount * 3 + 2, mockWriter.WrittenObjects.Count);
        }
        private void VerifyRangeRecordWasWritten(MockFontWriter mockWriter, int iteration, RangeRecord rangeRecord)
        {
            int startingIndex = (iteration * 3) + 2;
            Assert.AreEqual(rangeRecord.StartGlyphID, mockWriter.WrittenObjects[startingIndex]);
            Assert.AreEqual(rangeRecord.EndGlyphID, mockWriter.WrittenObjects[startingIndex + 1]);
            Assert.AreEqual(rangeRecord.StartCoverageIndex, mockWriter.WrittenObjects[startingIndex + 2]);
        }
    }
}
