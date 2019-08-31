using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.Offset.Mock
{
    using OpenType.Mock;

    public class Table : Offset.Table
    {
        public static Offset.Table One => new Table(TableRecord.SampleSetOne)
        {
            SfntVersion = 102345,
            EntrySelector = 1600,
            RangeShift = 40000,
            SearchRange = 1,
        };
        public static Offset.Table Two => new Table(TableRecord.SampleSetThree)
        {
            SfntVersion = 654,
            EntrySelector = 20,
            RangeShift = 10,
            SearchRange = 1
        };
        public static Offset.Table Three => new Table(TableRecord.SampleSetOne)
        {
            SfntVersion = 3216,
            EntrySelector = 55000,
            RangeShift = 47,
            SearchRange = 1
        };
        public static Offset.Table Four => new Table(TableRecord.SampleSetTwo)
        {
            SfntVersion = 7536,
            EntrySelector = 2200,
            RangeShift = 12345,
            SearchRange = 1
        };
        public static Offset.Table Five => new Table(TableRecord.Samples)
        {
            SfntVersion = 15986,
            EntrySelector = 10000,
            RangeShift = 65535,
            SearchRange = 1
        };

        public static Offset.Table Misordered => new Table(TableRecord.MisorderedSet);

        public static List<Offset.Table> Samples => new List<Offset.Table>
        {
            One,
            Two,
            Three,
            Four,
            Five
        };

        public static IEnumerable<object[]> DynamicData => Samples.Select(table => new object[] { table });

        public static void PrimeTableToRead(IFontReader mockReader, Offset.Table toPrime)
        {
            mockReader.PreparedUInt32s.Enqueue(toPrime.SfntVersion);
            mockReader.PreparedUInt16s.Enqueue(toPrime.NumberOfTables);
            mockReader.PreparedUInt16s.Enqueue(toPrime.SearchRange);
            mockReader.PreparedUInt16s.Enqueue(toPrime.EntrySelector);
            mockReader.PreparedUInt16s.Enqueue(toPrime.RangeShift);

            foreach (Offset.TableRecord record in toPrime.Records)
                PrimeTableRecord(mockReader, record);
        }
        private static void PrimeTableRecord(IFontReader mockReader, Offset.TableRecord record)
        {
            mockReader.PreparedTags.Enqueue(record.Tag);
            mockReader.PreparedUInt32s.Enqueue(record.Checksum);
            mockReader.PreparedUInt32s.Enqueue(record.Offset);
            mockReader.PreparedUInt32s.Enqueue(record.Length);
        }

        public static void VerifyTablesAreEqual(Offset.Table expected, Offset.Table actual)
        {
            Assert.AreEqual(expected.SfntVersion, actual.SfntVersion);
            Assert.AreEqual(expected.NumberOfTables, actual.NumberOfTables);
            Assert.AreEqual(expected.RangeShift, actual.RangeShift);
            Assert.AreEqual(expected.SearchRange, actual.SearchRange);
            Assert.AreEqual(expected.EntrySelector, actual.EntrySelector);

            Assert.AreEqual(expected.Records.Count, actual.Records.Count);
            int currentIndex = 0;
            foreach (Offset.TableRecord expectedRecord in expected.Records)
            {
                Offset.TableRecord actualRecord = actual.Records[currentIndex++];

                Assert.AreEqual(expectedRecord.Tag, actualRecord.Tag);
                Assert.AreEqual(expectedRecord.Checksum, actualRecord.Checksum);
                Assert.AreEqual(expectedRecord.Length, actualRecord.Length);
                Assert.AreEqual(expectedRecord.Offset, actualRecord.Offset);
            }
        }

        public static void VerifyTableWasWritten(IFontWriter mockWriter, Offset.Table expected)
        {
            Assert.AreEqual(expected.SfntVersion, mockWriter.WrittenObjects[0]);
            Assert.AreEqual(expected.NumberOfTables, mockWriter.WrittenObjects[1]);
            Assert.AreEqual(expected.SearchRange, mockWriter.WrittenObjects[2]);
            Assert.AreEqual(expected.EntrySelector, mockWriter.WrittenObjects[3]);
            Assert.AreEqual(expected.RangeShift, mockWriter.WrittenObjects[4]);

            int currentIndex = 5;
            for (int i = 0; i < expected.NumberOfTables; i++)
                currentIndex = VerifyTableRecordWasWritten(mockWriter, currentIndex, expected.Records[i]);
            Assert.AreEqual(currentIndex, mockWriter.WrittenObjects.Count);
        }
        private static int VerifyTableRecordWasWritten(IFontWriter mockWriter, int startIndex, Offset.TableRecord record)
        {
            Assert.AreEqual(record.Tag, mockWriter.WrittenObjects[startIndex]);
            Assert.AreEqual(record.Checksum, mockWriter.WrittenObjects[startIndex + 1]);
            return startIndex + 2;
        }

        private Table(List<Offset.TableRecord> tableRecords)
        {
            Records.AddRange(tableRecords);
            NumberOfTables = (ushort)Records.Count;
        }
    }
}
