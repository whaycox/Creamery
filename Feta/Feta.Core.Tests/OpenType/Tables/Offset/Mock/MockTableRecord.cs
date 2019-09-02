using System.Collections.Generic;

namespace Feta.OpenType.Tables.Offset.Mock
{
    using Domain;

    public class MockTableRecord : TableRecord
    {
        public static TableRecord One => new MockTableRecord() { Tag = "ZXY1", Offset = 120, Checksum = 34, Length = 1000 };
        public static TableRecord Two => new MockTableRecord() { Tag = "oeui", Offset = 444, Checksum = 488, Length = 12345 };
        public static TableRecord Three => new MockTableRecord() { Tag = "ABCD", Offset = 555, Checksum = 6543, Length = 5000 };
        public static TableRecord Four => new MockTableRecord() { Tag = "AoeU", Offset = 10000, Checksum = 1234, Length = 12000 };
        public static TableRecord Five => new MockTableRecord() { Tag = "IjkL", Offset = 1, Checksum = 9999, Length = 9876 };

        public static List<TableRecord> Samples => new List<TableRecord>
        {
            Three,
            Four,
            Five,
            One,
            Two,
        };
        public static List<TableRecord> SampleSetOne => new List<TableRecord>
        {
            Three,
            Five,
            One,
        };
        public static List<TableRecord> SampleSetTwo => new List<TableRecord>
        {
            Four,
            One,
            Two,
        };
        public static List<TableRecord> SampleSetThree => new List<TableRecord>
        {
            Five,
            One,
            Two,
        };

        public static List<TableRecord> MisorderedSet => new List<TableRecord>
        {
            Two,
            One,
        };
    }
}
