using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class NullableShortValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.SmallInt;
        public override object Content => Short;
        public short? Short { get; set; }
    }
    public class ShortValue : NullableShortValue
    { }
}
