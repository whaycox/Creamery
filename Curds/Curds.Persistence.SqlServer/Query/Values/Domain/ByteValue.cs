using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class ByteValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.TinyInt;
        public override object Content => Byte;
        public byte? Byte { get; set; }
    }
}
