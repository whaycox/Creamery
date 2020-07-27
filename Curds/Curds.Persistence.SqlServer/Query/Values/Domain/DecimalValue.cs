using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class NullableDecimalValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Decimal;
        public override object Content => Decimal;
        public decimal? Decimal { get; set; }
    }
    public class DecimalValue : NullableDecimalValue
    { }
}
