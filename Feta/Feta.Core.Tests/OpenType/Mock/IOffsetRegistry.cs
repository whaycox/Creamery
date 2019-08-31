using Feta.OpenType.Abstraction;
using System.Collections.Generic;

namespace Feta.OpenType.Mock
{
    public class IOffsetRegistry : Abstraction.IOffsetRegistry
    {
        public List<(uint offset, TableParseDelegate parseDelegate)> RegisteredParsers = new List<(uint offset, TableParseDelegate parseDelegate)>();
        public void RegisterParser(uint offset, TableParseDelegate parseDelegate) => RegisteredParsers.Add((offset, parseDelegate));

        public TableParseDelegate ParserToRetrieve = null;
        public List<uint> ParserOffsetsRetrieved = new List<uint>();
        public TableParseDelegate RetrieveParser(uint offset)
        {
            ParserOffsetsRetrieved.Add(offset);
            return ParserToRetrieve;
        }

        public List<(object key, uint offset)> StartsRegistered = new List<(object key, uint offset)>();
        public void RegisterStart(object key, uint offset) => StartsRegistered.Add((key, offset));

        public List<(object key, uint offset)> EndsRegistered = new List<(object key, uint offset)>();
        public void RegisterEnd(object key, uint offset) => EndsRegistered.Add((key, offset));

        public uint OffsetToRetrieve = 0;
        public List<object> OffsetKeysRetrieved = new List<object>();
        public uint RetrieveOffset(object key)
        {
            OffsetKeysRetrieved.Add(key);
            return OffsetToRetrieve;
        }

        public uint LengthToRetrieve = 0;
        public List<object> LengthKeysRetrieved = new List<object>();
        public uint RetrieveLength(object key)
        {
            LengthKeysRetrieved.Add(key);
            return LengthToRetrieve;
        }
    }
}
