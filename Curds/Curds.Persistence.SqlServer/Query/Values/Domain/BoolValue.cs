using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class BoolValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Bit;
        public override object Content => Bool;
        public bool? Bool { get; set; }
    }
}
