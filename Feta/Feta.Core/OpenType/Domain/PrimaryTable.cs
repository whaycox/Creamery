namespace Feta.OpenType.Domain
{
    public abstract class PrimaryTable : BaseTable
    {
        public const int RoundBytes = 4;

        public uint PaddedBytes { get; set; }
        public string Tag { get; }

        public PrimaryTable(string tag)
        {
            Tag = tag;
        }
    }
}
