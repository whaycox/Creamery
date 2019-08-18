using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Tables.Offset
{
    using Enumerations;

    public class Mock : Table
    {
        public static Table One => new Mock()
        {
            SfntVersion = SfntVersion.TrueTypeOutlines,
            EntrySelector = 1600,
            RangeShift = 40000,
            SearchRange = 1,
        };
        public static Table Two => new Mock()
        {
            SfntVersion = SfntVersion.CffDataOneAndTwo,
            EntrySelector = 20,
            RangeShift = 10,
            SearchRange = 1
        };
        public static Table Three => new Mock()
        {
            SfntVersion = SfntVersion.TrueTypeOutlines,
            EntrySelector = 55000,
            RangeShift = 47,
            SearchRange = 1
        };
        public static Table Four => new Mock()
        {
            SfntVersion = SfntVersion.TrueTypeOutlines,
            EntrySelector = 2200,
            RangeShift = 12345,
            SearchRange = 1
        };
        public static Table Five => new Mock()
        {
            SfntVersion = SfntVersion.CffDataOneAndTwo,
            EntrySelector = 10000,
            RangeShift = 65535,
            SearchRange = 1
        };

        public static Table[] Samples => new Table[]
        {
            One,
            Two,
            Three,
            Four,
            Five
        };

        public static IEnumerable<object[]> DynamicData => Samples.Select(table => new object[] { table });

        public static void VerifyEqual(Table expected, Table actual)
        {
            Assert.AreEqual(expected.SfntVersion, actual.SfntVersion);
            Assert.AreEqual(expected.NumberOfTables, actual.NumberOfTables);
            Assert.AreEqual(expected.RangeShift, actual.RangeShift);
            Assert.AreEqual(expected.SearchRange, actual.SearchRange);
            Assert.AreEqual(expected.EntrySelector, actual.EntrySelector);

            Assert.AreEqual(expected.Records.Count, actual.Records.Count);
            int currentIndex = 0;
            foreach (TableRecord expectedRecord in expected.Records.OrderBy(r => r.Tag))
            {
                TableRecord actualRecord = actual.Records[currentIndex++];

                Assert.AreEqual(expectedRecord.Tag, actualRecord.Tag);
                Assert.AreEqual(expectedRecord.Checksum, actualRecord.Checksum);
                Assert.AreEqual(expectedRecord.Length, actualRecord.Length);
                Assert.AreEqual(expectedRecord.Offset, actualRecord.Offset);
            }
        }

        private Mock()
        {
            Records.AddRange(MockTableRecord.Samples);
            NumberOfTables = (ushort)Records.Count;
        }
    }
}
