namespace Feta.OpenType.Tables.Coverage.Domain
{
    public class RangeRecord
    {
        public ushort StartGlyphID { get; set; }
        public ushort EndGlyphID { get; set; }
        public ushort StartCoverageIndex { get; set; }
    }
}
