using System;
using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class DateTimeValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.DateTime;
        public override object Content => DateTime;
        public DateTime? DateTime { get; set; }
    }
}
