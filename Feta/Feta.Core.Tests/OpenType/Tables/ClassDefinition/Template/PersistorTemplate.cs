using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Feta.OpenType.Tables.ClassDefinition.Template
{
    using OpenType.Domain;
    using Tables.Template;
    using Domain;
    using Implementation;

    public abstract class PersistorTemplate<T> : ITablePersistorTemplate<ClassDefinitionPersistor<T>, ClassDefinitionTable>
        where T : PrimaryTable
    {
        protected void PrimeReader(ClassDefinitionTable sample)
        {
            MockReader.PreparedUInt16s.Enqueue(sample.ClassFormat);
            switch (sample.ClassFormat)
            {
                case 1:
                    PrimeFormatOne(sample);
                    break;
                case 2:
                    PrimeFormatTwo(sample);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
        private void PrimeFormatOne(ClassDefinitionTable sample)
        {
            MockReader.PreparedUInt16s.Enqueue(sample.StartGlyphID);
            MockReader.PreparedUInt16s.Enqueue(sample.GlyphCount);
            for (int i = 0; i < sample.GlyphCount; i++)
                MockReader.PreparedUInt16s.Enqueue(sample.ClassArrayValues[i]);
        }
        private void PrimeFormatTwo(ClassDefinitionTable sample)
        {
            MockReader.PreparedUInt16s.Enqueue(sample.ClassRangeCount);
            for (int i = 0; i < sample.ClassRangeCount; i++)
                PrimeClassRangeRecord(sample.ClassRangeRecords[i]);
        }
        private void PrimeClassRangeRecord(ClassRangeRecord classRangeRecord)
        {
            MockReader.PreparedUInt16s.Enqueue(classRangeRecord.StartGlyphID);
            MockReader.PreparedUInt16s.Enqueue(classRangeRecord.EndGlyphID);
            MockReader.PreparedUInt16s.Enqueue(classRangeRecord.Class);
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

        protected void VerifyTableWasWritten(ClassDefinitionTable expected)
        {
            Assert.AreEqual(expected.ClassFormat, MockWriter.WrittenObjects[0]);
            switch(expected.ClassFormat)
            {
                case 1:
                    VerifyFormatOneWasWritten(expected);
                    break;
                case 2:
                    VerifyFormatTwoWasWritten(expected);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
        private void VerifyFormatOneWasWritten(ClassDefinitionTable expected)
        {
            Assert.AreEqual(expected.StartGlyphID, MockWriter.WrittenObjects[1]);
            Assert.AreEqual(expected.GlyphCount, MockWriter.WrittenObjects[2]);
            for (int i = 0; i < expected.GlyphCount; i++)
                Assert.AreEqual(expected.ClassArrayValues[i], MockWriter.WrittenObjects[i + 3]);
        }
        private void VerifyFormatTwoWasWritten(ClassDefinitionTable expected)
        {
            Assert.AreEqual(expected.ClassRangeCount, MockWriter.WrittenObjects[1]);
            for (int i = 0; i < expected.ClassRangeCount; i++)
                VerifyClassRangeRecordWasWritten((i * 3) + 2, expected.ClassRangeRecords[i]);
        }
        private void VerifyClassRangeRecordWasWritten(int startIndex, ClassRangeRecord classRangeRecord)
        {
            Assert.AreEqual(classRangeRecord.StartGlyphID, MockWriter.WrittenObjects[startIndex]);
            Assert.AreEqual(classRangeRecord.EndGlyphID, MockWriter.WrittenObjects[startIndex + 1]);
            Assert.AreEqual(classRangeRecord.Class, MockWriter.WrittenObjects[startIndex + 2]);
        }
    }
}
