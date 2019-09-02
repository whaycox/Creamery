namespace Feta.OpenType.Tables.Coverage.Mock
{
    using Domain;

    public class MockRangeRecord : RangeRecord
    {
        public static RangeRecord One => new MockRangeRecord() { StartGlyphID = 1, EndGlyphID = 2, StartCoverageIndex = 4 };

        public static RangeRecord[] Records(int numberOfRecords)
        {
            RangeRecord[] toReturn = new RangeRecord[numberOfRecords];
            for (int i = 0; i < numberOfRecords; i++)
                toReturn[i] = One;
            return toReturn;
        }
    }
}
