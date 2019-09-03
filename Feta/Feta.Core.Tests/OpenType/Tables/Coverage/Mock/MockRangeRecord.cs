namespace Feta.OpenType.Tables.Coverage.Mock
{
    using Domain;

    public class MockRangeRecord : RangeRecord
    {
        public static RangeRecord One => new MockRangeRecord() { StartGlyphID = 1, EndGlyphID = 5, StartCoverageIndex = 0 };
        public static RangeRecord Two => new MockRangeRecord() { StartGlyphID = 10, EndGlyphID = 20, StartCoverageIndex = 5 };
        public static RangeRecord Three => new MockRangeRecord() { StartGlyphID = 50, EndGlyphID = 70, StartCoverageIndex = 16 };
        public static RangeRecord Four => new MockRangeRecord() { StartGlyphID = 100, EndGlyphID = 200, StartCoverageIndex = 37 };
        public static RangeRecord Five => new MockRangeRecord() { StartGlyphID = 1000, EndGlyphID = 1050, StartCoverageIndex = 138 };

        public static RangeRecord[] Samples => new RangeRecord[]
        {
            One,
            Two,
            Three,
            Four,
            Five,
        };

        public static ushort MisorderedLength => (ushort)Misordered.Length;
        public static RangeRecord[] Misordered => new RangeRecord[]
        {
            Two,
            One,
        };

        public static ushort OverlappingLength => (ushort)Overlapping.Length;
        public static RangeRecord[] Overlapping => new RangeRecord[]
        {
            One,
            new MockRangeRecord() { StartGlyphID = 5, EndGlyphID = 15, StartCoverageIndex = 5 },
        };

        public static ushort BadStartCoverageIndexLength => (ushort)BadStartCoverageIndex.Length;
        public static RangeRecord[] BadStartCoverageIndex => new RangeRecord[]
        {
            One,
            new MockRangeRecord() { StartGlyphID = 10, EndGlyphID = 20, StartCoverageIndex = 6 },
        };

        public static RangeRecord[] Records(int number)
        {
            RangeRecord[] toReturn = new RangeRecord[number];

            ushort coverageIndex = 0;
            for (int i = 0; i < number; i++)
            {
                RangeRecord toAdd = new RangeRecord()
                {
                    StartGlyphID = coverageIndex,
                    EndGlyphID = (ushort)(coverageIndex + RecordsRange),
                    StartCoverageIndex = coverageIndex,
                };
                toReturn[i] = toAdd;
                coverageIndex += RecordsRange + 1;
            }

            return toReturn;
        }
        private const int RecordsRange = 5;
    }
}
