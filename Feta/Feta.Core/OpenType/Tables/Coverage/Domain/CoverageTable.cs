namespace Feta.OpenType.Tables.Coverage.Domain
{
    using OpenType.Domain;

    public class CoverageTable : BaseTable
    {
        public ushort Format { get; set; }

        #region Format One
        public ushort GlyphCount { get; set; }
        public ushort[] GlyphArray { get; set; }
        #endregion

        #region Format Two
        public ushort RangeCount { get; set; }
        public RangeRecord[] RangeRecords { get; set; }
        #endregion
    }
}
