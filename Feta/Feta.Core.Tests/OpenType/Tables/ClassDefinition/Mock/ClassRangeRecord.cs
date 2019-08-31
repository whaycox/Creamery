namespace Feta.OpenType.Tables.ClassDefinition.Mock
{
    public class ClassRangeRecord : ClassDefinition.ClassRangeRecord
    {
        public static ClassDefinition.ClassRangeRecord One => new ClassRangeRecord() { StartGlyphID = 23, EndGlyphID = 45, Class = 1 };
        public static ClassDefinition.ClassRangeRecord Two => new ClassRangeRecord() { StartGlyphID = 50, EndGlyphID = 65, Class = 2 };
        public static ClassDefinition.ClassRangeRecord Three => new ClassRangeRecord() { StartGlyphID = 100, EndGlyphID = 450, Class = 5 };
        public static ClassDefinition.ClassRangeRecord Four => new ClassRangeRecord() { StartGlyphID = 987, EndGlyphID = 1300, Class = 3 };
        public static ClassDefinition.ClassRangeRecord Five => new ClassRangeRecord() { StartGlyphID = 1500, EndGlyphID = 45000, Class = 4 };

        public static ushort SampleCount => (ushort)Samples.Length;
        public static ClassDefinition.ClassRangeRecord[] Samples => new ClassDefinition.ClassRangeRecord[]
        {
            One,
            Two,
            Three,
            Four,
            Five
        };
    }
}
