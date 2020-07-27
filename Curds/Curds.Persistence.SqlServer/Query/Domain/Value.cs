using System;

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
}
