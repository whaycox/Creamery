using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Mock
{
    public class IFontReader : Abstraction.IFontReader
    {
        public Abstraction.ITableCollection Tables { get; set; }
        public Abstraction.IOffsetRegistry Offsets { get; set; }

        public bool IsConsumed { get; set; }
        public uint CurrentOffset { get; set; }

        public Queue<byte> PreparedBytes = new Queue<byte>();
        public byte ReadByte() => PreparedBytes.Dequeue();

        public Queue<string> PreparedTags = new Queue<string>();
        public string ReadTag() => PreparedTags.Dequeue();

        public Queue<ushort> PreparedUInt16s = new Queue<ushort>();
        public ushort ReadUInt16() => PreparedUInt16s.Dequeue();

        public Queue<uint> PreparedUInt32s = new Queue<uint>();
        public uint ReadUInt32() => PreparedUInt32s.Dequeue();
    }
}
