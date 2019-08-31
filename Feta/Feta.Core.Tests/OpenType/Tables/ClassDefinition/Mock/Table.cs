using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.ClassDefinition.Mock
{
    public class Table : ClassDefinition.Table
    {
        public static ClassDefinition.Table One => new Table() { ClassFormat = 1, StartGlyphID = 25, GlyphCount = 46, ClassArrayValues = MockClassValues(46) };
        public static ClassDefinition.Table Two => new Table() { ClassFormat = 1, StartGlyphID = 400, GlyphCount = 100, ClassArrayValues = MockClassValues(100) };
        public static ClassDefinition.Table Three => new Table() { ClassFormat = 2, ClassRangeCount = ClassRangeRecord.SampleCount, ClassRangeRecords = ClassRangeRecord.Samples };
        public static ClassDefinition.Table Four => new Table() { ClassFormat = 2, ClassRangeCount = 3, ClassRangeRecords = new ClassDefinition.ClassRangeRecord[] { ClassRangeRecord.One, ClassRangeRecord.Four, ClassRangeRecord.Five } };
        public static ClassDefinition.Table Five => new Table() { ClassFormat = 2, ClassRangeCount = 2, ClassRangeRecords = new ClassDefinition.ClassRangeRecord[] { ClassRangeRecord.Three, ClassRangeRecord.Five } };

        public static IEnumerable<object[]> DynamicData => Samples.Select(sample => new object[] { sample });
        public static ClassDefinition.Table[] Samples => new ClassDefinition.Table[]
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
