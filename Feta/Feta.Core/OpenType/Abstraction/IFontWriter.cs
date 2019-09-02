namespace Feta.OpenType.Abstraction
{
    public delegate T DeferredValueSelector<T>(ITableCollection tableCollection, IOffsetRegistry offsetRegistry);

    public interface IFontWriter
    {
        ITableCollection Tables { get; }
        IOffsetRegistry Offsets { get; }

        byte[] GetBytes();

        void WriteUInt16(ushort value);
        void WriteUInt32(uint value);
        void WriteTag(string tag);

        void DeferWriteUInt16(DeferredValueSelector<ushort> deferrer);
        void DeferWriteUInt32(DeferredValueSelector<uint> deferrer);
    }
}
