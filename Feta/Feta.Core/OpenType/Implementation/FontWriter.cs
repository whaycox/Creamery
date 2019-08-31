using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Implementation
{
    using Abstraction;

    public class FontWriter : IFontWriter
    {
        private const int TagLength = 4;

        private List<byte> Bytes { get; set; } = new List<byte>();
        private int CurrentIndex => Bytes.Count;

        private Dictionary<int, DeferredValueSelector<ushort>> DeferredUInt16s { get; set; } = new Dictionary<int, DeferredValueSelector<ushort>>();
        private Dictionary<int, DeferredValueSelector<uint>> DeferredUInt32s { get; set; } = new Dictionary<int, DeferredValueSelector<uint>>();

        public ITableCollection Tables { get; }
        public IOffsetRegistry Offsets { get; }

        public FontWriter(ITableCollection tableCollection, IOffsetRegistry offsetRegistry)
        {
            Tables = tableCollection;
            Offsets = offsetRegistry;
        }

        public byte[] GetBytes()
        {
            EvaluateDeferredWrites();
            byte[] toReturn = Bytes.ToArray();
            Bytes = new List<byte>();
            return toReturn;
        }
        private void EvaluateDeferredWrites()
        {
            foreach (var deferredWrite in DeferredUInt16s)
                OverwriteBytes(deferredWrite.Key, EncodeUInt16(deferredWrite.Value(Tables, Offsets)));
            foreach (var deferredWrite in DeferredUInt32s)
                OverwriteBytes(deferredWrite.Key, EncodeUInt32(deferredWrite.Value(Tables, Offsets)));
        }
        private void OverwriteBytes(int startIndex, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                Bytes[startIndex + i] = bytes[i];
        }

        private byte[] EncodeUInt16(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }
        private byte[] EncodeUInt32(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public void WriteUInt16(ushort value) => Bytes.AddRange(EncodeUInt16(value));
        public void WriteUInt32(uint value) => Bytes.AddRange(EncodeUInt32(value));
        public void WriteTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag) || tag.Length != TagLength)
                throw new ArgumentException(nameof(tag));

            byte[] bytes = Encoding.ASCII.GetBytes(tag);
            Bytes.AddRange(bytes);
        }

        public void DeferWriteUInt16(DeferredValueSelector<ushort> deferrer)
        {
            int deferredIndex = CurrentIndex;
            WriteUInt16(default(ushort));
            DeferredUInt16s.Add(deferredIndex, deferrer);
        }
        public void DeferWriteUInt32(DeferredValueSelector<uint> deferrer)
        {
            int deferredIndex = CurrentIndex;
            WriteUInt32(default(uint));
            DeferredUInt32s.Add(deferredIndex, deferrer);
        }
    }
}
