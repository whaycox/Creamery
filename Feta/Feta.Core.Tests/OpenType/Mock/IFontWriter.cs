using System;
using System.Collections.Generic;
using System.IO;
using Feta.OpenType.Abstraction;

namespace Feta.OpenType.Mock
{
    public class IFontWriter : Abstraction.IFontWriter
    {
        public Abstraction.ITableCollection Tables => throw new NotImplementedException();
        public Abstraction.IOffsetRegistry Offsets => throw new NotImplementedException();

        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public List<object> WrittenObjects = new List<object>();

        public List<string> WrittenTags = new List<string>();
        public void WriteTag(string tag)
        {
            WrittenObjects.Add(tag);
            WrittenTags.Add(tag);
        }

        public List<ushort> WrittenUInt16s = new List<ushort>();
        public void WriteUInt16(ushort value)
        {
            WrittenObjects.Add(value);
            WrittenUInt16s.Add(value);
        }

        public List<uint> WrittenUInt32s = new List<uint>();
        public void WriteUInt32(uint value)
        {
            WrittenObjects.Add(value);
            WrittenUInt32s.Add(value);
        }

        public List<DeferredValueSelector<ushort>> DeferredUInt16s = new List<DeferredValueSelector<ushort>>();
        public void DeferWriteUInt16(DeferredValueSelector<ushort> deferrer) => DeferredUInt16s.Add(deferrer);

        public List<DeferredValueSelector<uint>> DeferredUInt32s = new List<DeferredValueSelector<uint>>();
        public void DeferWriteUInt32(DeferredValueSelector<uint> deferrer) => DeferredUInt32s.Add(deferrer);
    }
}
