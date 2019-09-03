using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.ClassDefinition.Template
{
    using Domain;
    using Feta.OpenType.Mock;
    using Implementation;
    using OpenType.Domain;
    using OpenType.Template;

    public abstract class ClassDefinitionPersistorTemplate<T> : ITablePersistorTemplate<ClassDefinitionPersistor<T>, ClassDefinitionTable>
        where T : PrimaryTable
    {
        protected override void PrimeTableToRead(MockFontReader mockReader, ClassDefinitionTable table)
        {
            mockReader.PreparedUInt16s.Enqueue(table.ClassFormat);
            switch (table.ClassFormat)
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
        private void PrimeFormatOne(MockFontReader mockReader, ClassDefinitionTable sample)
        {
            mockReader.PreparedUInt16s.Enqueue(sample.StartGlyphID);
            mockReader.PreparedUInt16s.Enqueue(sample.GlyphCount);
            for (int i = 0; i < sample.GlyphCount; i++)
                mockReader.PreparedUInt16s.Enqueue(sample.ClassArrayValues[i]);
        }
        private void PrimeFormatTwo(MockFontReader mockReader, ClassDefinitionTable sample)
        {
            mockReader.PreparedUInt16s.Enqueue(sample.ClassRangeCount);
            for (int i = 0; i < sample.ClassRangeCount; i++)
                PrimeClassRangeRecord(mockReader, sample.ClassRangeRecords[i]);
        }
        private void PrimeClassRangeRecord(MockFontReader mockReader, ClassRangeRecord classRangeRecord)
        {
            mockReader.PreparedUInt16s.Enqueue(classRangeRecord.StartGlyphID);
            mockReader.PreparedUInt16s.Enqueue(classRangeRecord.EndGlyphID);
            mockReader.PreparedUInt16s.Enqueue(classRangeRecord.Class);
        }

        protected override void VerifyTablesAreEqual(ClassDefinitionTable expected, ClassDefinitionTable actual)
        {
            Assert.AreEqual(expected.ClassFormat, actual.ClassFormat);

            Assert.AreEqual(expected.StartGlyphID, actual.StartGlyphID);
            Assert.AreEqual(expected.GlyphCount, actual.GlyphCount);
            if (expected.ClassArrayValues != null)
            {
                Assert.AreEqual(expected.ClassArrayValues.Length, actual.ClassArrayValues.Length);
                for (int i = 0; i < expected.ClassArrayValues.Length; i++)
                    Assert.AreEqual(expected.ClassArrayValues[i], actual.ClassArrayValues[i]);
            }
            else
                Assert.IsNull(actual.ClassArrayValues);

            Assert.AreEqual(expected.ClassRangeCount, actual.ClassRangeCount);
            if (expected.ClassRangeRecords != null)
            {
                Assert.AreEqual(expected.ClassRangeRecords.Length, actual.ClassRangeRecords.Length);
                for (int i = 0; i < expected.ClassRangeRecords.Length; i++)
                    VerifyClassRangeRecord(expected.ClassRangeRecords[i], actual.ClassRangeRecords[i]);
            }
            else
                Assert.IsNull(actual.ClassRangeRecords);
        }
        private void VerifyClassRangeRecord(ClassRangeRecord expected, ClassRangeRecord actual)
        {
            Assert.AreEqual(expected.StartGlyphID, actual.StartGlyphID);
            Assert.AreEqual(expected.EndGlyphID, actual.EndGlyphID);
            Assert.AreEqual(expected.Class, actual.Class);
        }

        protected override void VerifyTableWasWritten(MockFontWriter mockWriter, ClassDefinitionTable expected)
        {
            Assert.AreEqual(expected.ClassFormat, mockWriter.WrittenObjects[0]);
            switch (expected.ClassFormat)
            {
                case 1:
                    VerifyFormatOneWasWritten(mockWriter, expected);
                    break;
                case 2:
                    VerifyFormatTwoWasWritten(mockWriter, expected);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
        private void VerifyFormatOneWasWritten(MockFontWriter mockWriter, ClassDefinitionTable expected)
        {
            Assert.AreEqual(expected.StartGlyphID, mockWriter.WrittenObjects[1]);
            Assert.AreEqual(expected.GlyphCount, mockWriter.WrittenObjects[2]);
            for (int i = 0; i < expected.GlyphCount; i++)
                Assert.AreEqual(expected.ClassArrayValues[i], mockWriter.WrittenObjects[i + 3]);
        }
        private void VerifyFormatTwoWasWritten(MockFontWriter mockWriter, ClassDefinitionTable expected)
        {
            Assert.AreEqual(expected.ClassRangeCount, mockWriter.WrittenObjects[1]);
            for (int i = 0; i < expected.ClassRangeCount; i++)
                VerifyClassRangeRecordWasWritten(mockWriter, (i * 3) + 2, expected.ClassRangeRecords[i]);
        }
        private void VerifyClassRangeRecordWasWritten(MockFontWriter mockWriter, int startIndex, ClassRangeRecord classRangeRecord)
        {
            Assert.AreEqual(classRangeRecord.StartGlyphID, mockWriter.WrittenObjects[startIndex]);
            Assert.AreEqual(classRangeRecord.EndGlyphID, mockWriter.WrittenObjects[startIndex + 1]);
            Assert.AreEqual(classRangeRecord.Class, mockWriter.WrittenObjects[startIndex + 2]);
        }
    }
}
