using System.Data;

namespace Curds.Persistence.Query.Domain
{
    public class NullableIntValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Int;
        public override object Content => Int;
        public int? Int { get; set; }
    }
    public class IntValue : NullableIntValue
    { }
}
