using System;
using System.Collections.Generic;

namespace Feta.OpenType.Implementation
{
    using Abstraction;
    using Domain;
    using Exceptions;

    public class OffsetRegistry : IOffsetRegistry
    {
        private Dictionary<uint, TableParseDelegate> Parsers { get; } = new Dictionary<uint, TableParseDelegate>();
        private Dictionary<object, OffsetRange> Offsets { get; } = new Dictionary<object, OffsetRange>();

        public void RegisterParser(uint offset, TableParseDelegate parser) => Parsers.Add(offset, parser);
        public TableParseDelegate RetrieveParser(uint offset) => Parsers[offset];

        public void RegisterStart(object key, uint offset) => Offsets.Add(key, new OffsetRange() { Start = offset });
        public void RegisterEnd(object key, uint offset)
        {
            if (!Offsets.TryGetValue(key, out OffsetRange range))
                throw new IncompleteOffsetException("Cannot register an end before a start");
            if (range.End.HasValue)
                throw new ArgumentException("Duplicate ends are not allowed");
            if (range.Start > offset)
                throw new ArgumentOutOfRangeException("Cannot have an end before the start");
            Offsets[key].End = offset;
        }

        public uint RetrieveOffset(object key) => Offsets[key].Start;
        public uint RetrieveLength(object key)
        {
            if (!Offsets.TryGetValue(key, out OffsetRange range))
                throw new IncompleteOffsetException("Cannot calculate length without a start");
            if (!range.End.HasValue)
                throw new IncompleteOffsetException("Cannot calculate length without an end");
            return range.End.Value - range.Start;
        }
    }
}
