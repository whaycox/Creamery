namespace Feta.OpenType.Tables.Coverage.Mock
{
    using Domain;

    public class MockTable : CoverageTable
    {
        public static CoverageTable One => new MockTable() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5) };
        public static CoverageTable Two => new MockTable() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5) };
        public static CoverageTable Three => new MockTable() { Format = 2, RangeCount = 3, RangeRecords = MockRangeRecord.Records(3) };
        public static CoverageTable Four => new MockTable() { Format = 2, RangeCount = 10, RangeRecords = MockRangeRecord.Records(10) };
        public static CoverageTable Five => new MockTable() { Format= 2, RangeCount = 50, RangeRecords = MockRangeRecord.Records(50) };

        public static CoverageTable[] Samples => new CoverageTable[]
        {
            One,
            Two,
            Three,
            Four,
            Five
        };

        private static ushort[] BuildGlyphArray(int numberOfGlyphs)
        {
            ushort[] toReturn = new ushort[numberOfGlyphs];
            for (int i = 0; i < numberOfGlyphs; i++)
                toReturn[i] = (ushort)i;
            return toReturn;
        }
    }
}
