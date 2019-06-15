using System.Collections.Generic;

namespace Gouda.Check.Data.Domain
{
    using Communication;
    using Communication.Domain;
    using Enumerations;

    public class LongSeries : Series<long>
    {
        public override SeriesType SeriesType => SeriesType.Long;

        public LongSeries(string name, long value)
            : base(name, value)
        { }

        public LongSeries(BufferReader parser)
            : base(parser.ParseString(), parser.ParseLong())
        { }

        protected override List<byte> ContentBuffer() => new List<byte>()
            .Append(Name)
            .Append(Value);
    }
}
