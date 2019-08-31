namespace Feta.OpenType.Tables.Coverage
{
    using OpenType.Domain;

    public class Table : BaseTable
    {
        public ushort Format { get; set; }

        #region Format One
        public ushort? GlyphCount { get; set; }
        public ushort[] GlyphArray { get; set; }
        #endregion

        #region Format Two
        public ushort? RangeCount { get; set; }
        public RangeRecord[] RangeRecords { get; set; }
        #endregion
    }
}
