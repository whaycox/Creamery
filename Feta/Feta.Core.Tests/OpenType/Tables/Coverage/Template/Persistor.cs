using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Coverage.Template
{
    using OpenType.Domain;
    using Tables.Template;

    public abstract class Persistor<T> : ITablePersistor<Coverage.Persistor<T>, Table>
        where T : PrimaryTable
    {
        //protected override List<Table> Samples => new List<Table>(Mock.Table.Samples);

        //protected override void VerifyTablesAreEqual(Table expected, Table actual)
        //{
        //    Assert.AreEqual(expected.Format, actual.Format);
        //    Assert.AreEqual(expected.GlyphCount, actual.GlyphCount);
        //    if (expected.GlyphArray != null)
        //    {
        //        Assert.AreEqual(expected.GlyphArray.Length, actual.GlyphArray.Length);
        //        for (int i = 0; i < expected.GlyphArray.Length; i++)
        //            Assert.AreEqual(expected.GlyphArray[i], actual.GlyphArray[i]);
        //    }
        //    else
        //        Assert.IsNull(actual.GlyphArray);

        //    Assert.AreEqual(expected.RangeCount, actual.RangeCount);
        //    if (expected.RangeRecords != null)
        //    {
        //        Assert.AreEqual(expected.RangeRecords.Length, actual.RangeRecords.Length);
        //        for (int i = 0; i < expected.RangeRecords.Length; i++)
        //            VerifyRangeRecordsAreEqual(expected.RangeRecords[i], actual.RangeRecords[i]);
        //    }
        //    else
        //        Assert.IsNull(actual.RangeRecords);
        //}
        //private void VerifyRangeRecordsAreEqual(RangeRecord expected, RangeRecord actual)
        //{
        //    Assert.AreEqual(expected.StartGlyphID, actual.StartGlyphID);
        //    Assert.AreEqual(expected.EndGlyphID, actual.EndGlyphID);
        //    Assert.AreEqual(expected.StartCoverageIndex, actual.StartCoverageIndex);
        //}
    }
}
