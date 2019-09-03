namespace Feta.OpenType.Tables.ClassDefinition.Mock
{
    using Domain;

    public class MockClassRangeRecord : ClassRangeRecord
    {
        public static ClassRangeRecord One => new MockClassRangeRecord() { StartGlyphID = 23, EndGlyphID = 45, Class = 1 };
        public static ClassRangeRecord Two => new MockClassRangeRecord() { StartGlyphID = 50, EndGlyphID = 65, Class = 2 };
        public static ClassRangeRecord Three => new MockClassRangeRecord() { StartGlyphID = 100, EndGlyphID = 450, Class = 5 };
        public static ClassRangeRecord Four => new MockClassRangeRecord() { StartGlyphID = 987, EndGlyphID = 1300, Class = 3 };
        public static ClassRangeRecord Five => new MockClassRangeRecord() { StartGlyphID = 1500, EndGlyphID = 45000, Class = 4 };

        public static ushort AllSamplesLength => (ushort)AllSamples.Length;
        public static ClassRangeRecord[] AllSamples => new ClassRangeRecord[]
        {
            One,
            Two,
            Three,
            Four,
            Five
        };

        public static ushort SampleSetOneLength => (ushort)SampleSetOne.Length;
        public static ClassRangeRecord[] SampleSetOne => new ClassRangeRecord[]
        {
            One,
            Four,
            Five,
        };

        public static ushort SampleSetTwoLength => (ushort)SampleSetTwo.Length;
        public static ClassRangeRecord[] SampleSetTwo => new ClassRangeRecord[]
        {
            Three,
            Five,
        };

        public static ushort MisorderedSamplesLength => (ushort)MisorderedSamples.Length;
        public static ClassRangeRecord[] MisorderedSamples => new ClassRangeRecord[]
        {
            Three,
            Two,
            One,
        };

        public static ushort OverlappingSamplesLength => (ushort)OverlappingSamples.Length;
        public static ClassRangeRecord[] OverlappingSamples => new ClassRangeRecord[]
        {
            One,
            new MockClassRangeRecord() { StartGlyphID = 45, EndGlyphID = 50, Class = 3},
            Two,
        };
    }
}
