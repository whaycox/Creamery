using System;
using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class DateTimeOffsetValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.DateTimeOffset;
        public override object Content => DateTimeOffset;
        public DateTimeOffset? DateTimeOffset { get; set; }
    }
}
