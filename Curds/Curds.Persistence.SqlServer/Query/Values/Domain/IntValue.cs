using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class IntValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Int;
        public override object Content => Int;
        public int? Int { get; set; }
    }
}
