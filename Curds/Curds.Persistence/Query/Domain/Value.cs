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

    public class IntValue : Value<SqlDbType>
    {
        public override SqlDbType DataType => SqlDbType.Int;
        public override object Content => Int;
        public int? Int { get; set; }
    }
}
