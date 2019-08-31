namespace Feta.OpenType.Tables.Coverage.Mock
{
    public class Table : Coverage.Table
    {
        public static Coverage.Table One => new Table() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5) };
        public static Coverage.Table Two => new Table() { Format = 1, GlyphCount = 5, GlyphArray = BuildGlyphArray(5) };
        public static Coverage.Table Three => new Table() { Format = 2, RangeCount = 3, RangeRecords = RangeRecord.Records(3) };
        public static Coverage.Table Four => new Table() { Format = 2, RangeCount = 10, RangeRecords = RangeRecord.Records(10) };
        public static Coverage.Table Five => new Table() { Format= 2, RangeCount = 50, RangeRecords = RangeRecord.Records(50) };

        public static Coverage.Table[] Samples => new Coverage.Table[]
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
