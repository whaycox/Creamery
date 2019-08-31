using System.Collections.Generic;

namespace Feta.OpenType.Tables.Offset.Mock
{
    public class TableRecord : Offset.TableRecord
    {
        public static Offset.TableRecord One => new TableRecord() { Tag = "ZXY1", Offset = 120, Checksum = 34, Length = 1000 };
        public static Offset.TableRecord Two => new TableRecord() { Tag = "oeui", Offset = 444, Checksum = 488, Length = 12345 };
        public static Offset.TableRecord Three => new TableRecord() { Tag = "ABCD", Offset = 555, Checksum = 6543, Length = 5000 };
        public static Offset.TableRecord Four => new TableRecord() { Tag = "AoeU", Offset = 10000, Checksum = 1234, Length = 12000 };
        public static Offset.TableRecord Five => new TableRecord() { Tag = "IjkL", Offset = 1, Checksum = 9999, Length = 9876 };

        public static List<Offset.TableRecord> Samples => new List<Offset.TableRecord>
        {
            Three,
            Four,
            Five,
            One,
            Two,
        };
        public static List<Offset.TableRecord> SampleSetOne => new List<Offset.TableRecord>
        {
            Three,
            Five,
            One,
        };
        public static List<Offset.TableRecord> SampleSetTwo => new List<Offset.TableRecord>
        {
            Four,
            One,
            Two,
        };
        public static List<Offset.TableRecord> SampleSetThree => new List<Offset.TableRecord>
        {
            Five,
            One,
            Two,
        };

        public static List<Offset.TableRecord> MisorderedSet => new List<Offset.TableRecord>
        {
            Two,
            One,
        };
    }
}
