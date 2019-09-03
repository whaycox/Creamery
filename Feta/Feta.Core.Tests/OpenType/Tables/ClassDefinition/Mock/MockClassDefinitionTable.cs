using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.ClassDefinition.Mock
{
    using Domain;

    public class MockClassDefinitionTable : ClassDefinitionTable
    {
        public static ClassDefinitionTable One => new MockClassDefinitionTable() { ClassFormat = 1, StartGlyphID = 25, GlyphCount = 46, ClassArrayValues = MockClassValues(46) };
        public static ClassDefinitionTable Two => new MockClassDefinitionTable() { ClassFormat = 1, StartGlyphID = 400, GlyphCount = 100, ClassArrayValues = MockClassValues(100) };
        public static ClassDefinitionTable Three => new MockClassDefinitionTable() { ClassFormat = 2, ClassRangeCount = MockClassRangeRecord.AllSamplesLength, ClassRangeRecords = MockClassRangeRecord.AllSamples };
        public static ClassDefinitionTable Four => new MockClassDefinitionTable() { ClassFormat = 2, ClassRangeCount = MockClassRangeRecord.SampleSetOneLength, ClassRangeRecords = MockClassRangeRecord.SampleSetOne };
        public static ClassDefinitionTable Five => new MockClassDefinitionTable() { ClassFormat = 2, ClassRangeCount = MockClassRangeRecord.SampleSetTwoLength, ClassRangeRecords = MockClassRangeRecord.SampleSetTwo };

        public static ClassDefinitionTable Misordered => new MockClassDefinitionTable() { ClassFormat = 2, ClassRangeCount = MockClassRangeRecord.MisorderedSamplesLength, ClassRangeRecords = MockClassRangeRecord.MisorderedSamples };
        public static ClassDefinitionTable Overlapping => new MockClassDefinitionTable() { ClassFormat = 2, ClassRangeCount = MockClassRangeRecord.OverlappingSamplesLength, ClassRangeRecords = MockClassRangeRecord.OverlappingSamples };

        public static IEnumerable<object[]> DynamicData => Samples.Select(sample => new object[] { sample });
        public static ClassDefinitionTable[] Samples => new ClassDefinitionTable[]
        {
            One,
            Two,
            Three,
            Four,
            Five
        };

        private static ushort[] MockClassValues(int length)
        {
            ushort[] toReturn = new ushort[length];
            for (int i = 0; i < length; i++)
                toReturn[i] = (ushort)(i % 4);
            return toReturn;
        }
    }
}
