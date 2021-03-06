﻿using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class LongValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.BigInt;
        public override object Content => Long;
        public long? Long { get; set; }
    }
}
