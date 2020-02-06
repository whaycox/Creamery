using System;
using System.Data;

namespace Curds.Persistence.Query.Domain
{
    public abstract class Value
    {
        public string Name { get; set; }
        public abstract object Content { get; }
    }

    public abstract class Value<TDataType> : Value
        where TDataType : Enum
    {
        public abstract TDataType DataType { get; }
    }

    public class StringValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.NVarChar;
        public override object Content => String;
        public string String { get; set; }
    }

    public class BoolValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Bit;
        public override object Content => Bool;
        public bool? Bool { get; set; }
    }

    public class IntValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Int;
        public override object Content => Int;
        public int? Int { get; set; }
    }

    public class DateTimeValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.DateTime;
        public override object Content => DateTime;
        public DateTime? DateTime { get; set; }
    }

    public class DateTimeOffsetValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.DateTimeOffset;
        public override object Content => DateTimeOffset;
        public DateTimeOffset? DateTimeOffset { get; set; }
    }

    public class DecimalValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Decimal;
        public override object Content => Decimal;
        public decimal? Decimal { get; set; }
    }

    public class DoubleValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Float;
        public override object Content => Double;
        public double? Double { get; set; }
    }
}
