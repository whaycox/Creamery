namespace Feta.OpenType.Abstraction
{
    public interface IFontReader
    {
        ITableCollection Tables { get; }
        IOffsetRegistry Offsets { get; }

        bool IsConsumed { get; }
        uint CurrentOffset { get; }

        byte ReadByte();
        ushort ReadUInt16();
        uint ReadUInt32();
        string ReadTag();
    }
}
