namespace Feta.OpenType.Tables.Coverage.Mock
{
    public class RangeRecord : Coverage.RangeRecord
    {
        public static Coverage.RangeRecord One => new RangeRecord() { StartGlyphID = 1, EndGlyphID = 2, StartCoverageIndex = 4 };

        public static Coverage.RangeRecord[] Records(int numberOfRecords)
        {
            Coverage.RangeRecord[] toReturn = new Coverage.RangeRecord[numberOfRecords];
            for (int i = 0; i < numberOfRecords; i++)
                toReturn[i] = One;
            return toReturn;
        }
    }
}
