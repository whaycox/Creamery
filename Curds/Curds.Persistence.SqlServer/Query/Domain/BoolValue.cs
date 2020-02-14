using System.Data;

namespace Curds.Persistence.Query.Domain
{
    public class NullableBoolValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Bit;
        public override object Content => Bool;
        public bool? Bool { get; set; }
    }
    public class BoolValue : NullableBoolValue
    { }
}
