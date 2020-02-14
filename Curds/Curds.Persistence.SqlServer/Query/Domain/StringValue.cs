using System.Data;

namespace Curds.Persistence.Query.Domain
{
    public class StringValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.NVarChar;
        public override object Content => String;
        public string String { get; set; }
    }
}
