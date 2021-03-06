﻿using System.Data;

namespace Curds.Persistence.Query.Values.Domain
{
    using Query.Domain;

    public class DoubleValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Float;
        public override object Content => Double;
        public double? Double { get; set; }
    }
}
