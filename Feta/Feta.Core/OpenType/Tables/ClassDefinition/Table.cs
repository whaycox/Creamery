namespace Feta.OpenType.Tables.ClassDefinition
{
    using OpenType.Domain;

    public class Table : BaseTable
    {
        public ushort ClassFormat { get; set; }

        #region Format One
        public ushort StartGlyphID { get; set; }
        public ushort GlyphCount { get; set; }
        public ushort[] ClassArrayValues { get; set; }
        #endregion

        #region Format Two
        public ushort ClassRangeCount { get; set; }
        public ClassRangeRecord[] ClassRangeRecords { get; set; }
        #endregion
    }
}
