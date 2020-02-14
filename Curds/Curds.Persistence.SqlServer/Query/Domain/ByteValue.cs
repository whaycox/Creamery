using System.Data;

namespace Curds.Persistence.Query.Domain
{
    public class NullableByteValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.TinyInt;
        public override object Content => Byte;
        public byte? Byte { get; set; }
    }
    public class ByteValue : NullableByteValue
    { }
}
