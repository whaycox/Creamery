namespace Feta.OpenType.Tables.Offset
{
    public class TableRecord
    {
        public string Tag { get; set; }
        public uint Checksum { get; set; }
        public uint Offset { get; set; }
        public uint Length { get; set; }

        public override string ToString() => $"{Tag} (0x{Offset:X2})|{Length}";
    }
}
