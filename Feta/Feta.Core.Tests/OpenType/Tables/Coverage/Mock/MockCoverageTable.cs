using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.Coverage.Mock
{
    using Domain;

    public class MockCoverageTable : CoverageTable
    {
        public static CoverageTable One => new MockCoverageTable() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5) };
        public static CoverageTable Two => new MockCoverageTable() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5) };
        public static CoverageTable Three => new MockCoverageTable() { Format = 2, RangeCount = 3, RangeRecords = MockRangeRecord.Records(3) };
        public static CoverageTable Four => new MockCoverageTable() { Format = 2, RangeCount = 10, RangeRecords = MockRangeRecord.Records(10) };
        public static CoverageTable Five => new MockCoverageTable() { Format = 2, RangeCount = 50, RangeRecords = MockRangeRecord.Records(50) };

        public static CoverageTable MisorderedGlyphArray => new MockCoverageTable() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5).Reverse().ToArray() };
        public static CoverageTable MisorderedRangeRecord => new MockCoverageTable() { Format = 2, RangeCount = MockRangeRecord.MisorderedLength, RangeRecords = MockRangeRecord.Misordered };
        public static CoverageTable OverlappingRangeRecord => new MockCoverageTable() { Format = 2, RangeCount = MockRangeRecord.OverlappingLength, RangeRecords = MockRangeRecord.Overlapping };
        public static CoverageTable BadStartCoverageIndex => new MockCoverageTable() { Format = 2, RangeCount = MockRangeRecord.BadStartCoverageIndexLength, RangeRecords = MockRangeRecord.BadStartCoverageIndex };

        public static IEnumerable<object[]> DynamicData => Samples.Select(s => new object[] { s });
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
