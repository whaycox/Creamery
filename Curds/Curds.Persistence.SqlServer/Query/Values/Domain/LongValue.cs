using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class NullableLongValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.BigInt;
        public override object Content => Long;
        public long? Long { get; set; }
    }
    public class LongValue : NullableLongValue
    { }
}
