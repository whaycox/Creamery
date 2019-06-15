using System.Collections.Generic;

namespace Gouda.Check.Data.Domain
{
    using Communication;
    using Communication.Domain;
    using Enumerations;

    public class IntSeries : Series<int>
    {
        public override SeriesType SeriesType => SeriesType.Int;

        public IntSeries(string name, int value)
            : base(name, value)
        { }

        public IntSeries(BufferReader parser)
            : base(parser.ParseString(), parser.ParseInt())
        { }

        protected override List<byte> ContentBuffer() => new List<byte>()
            .Append(Name)
            .Append(Value);
    }
}
