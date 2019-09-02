using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.Offset.Mock
{
    using OpenType.Mock;
    using Domain;

    public class MockTable : OffsetTable
    {
        public static OffsetTable One => new MockTable(MockTableRecord.SampleSetOne)
        {
            SfntVersion = 102345,
            EntrySelector = 1600,
            RangeShift = 40000,
            SearchRange = 1,
        };
        public static OffsetTable Two => new MockTable(MockTableRecord.SampleSetThree)
        {
            SfntVersion = 654,
            EntrySelector = 20,
            RangeShift = 10,
            SearchRange = 1
        };
        public static OffsetTable Three => new MockTable(MockTableRecord.SampleSetOne)
        {
            SfntVersion = 3216,
            EntrySelector = 55000,
            RangeShift = 47,
            SearchRange = 1
        };
        public static OffsetTable Four => new MockTable(MockTableRecord.SampleSetTwo)
        {
            SfntVersion = 7536,
            EntrySelector = 2200,
            RangeShift = 12345,
            SearchRange = 1
        };
        public static OffsetTable Five => new MockTable(MockTableRecord.Samples)
        {
            SfntVersion = 15986,
            EntrySelector = 10000,
            RangeShift = 65535,
            SearchRange = 1
        };

        public static OffsetTable Misordered => new MockTable(MockTableRecord.MisorderedSet);

        public static List<OffsetTable> Samples => new List<OffsetTable>
        {
            One,
            Two,
            Three,
            Four,
            Five
        };

        public static IEnumerable<object[]> DynamicData => Samples.Select(table => new object[] { table });

        private MockTable(List<TableRecord> tableRecords)
        {
            Records.AddRange(tableRecords);
            NumberOfTables = (ushort)Records.Count;
        }
    }
}
