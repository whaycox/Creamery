using System.Collections.Generic;

namespace Gouda.Check.Data.Domain
{
    using Communication;
    using Communication.Domain;
    using Enumerations;

    public class DecimalSeries : Series<decimal>
    {
        public override SeriesType SeriesType => SeriesType.Decimal;

        public DecimalSeries(string name, decimal value)
            : base(name, value)
        { }

        public DecimalSeries(BufferReader parser)
            : base(parser.ParseString(), parser.ParseDecimal())
        { }

        protected override List<byte> ContentBuffer() => new List<byte>()
            .Append(Name)
            .Append(Value);
    }
}
