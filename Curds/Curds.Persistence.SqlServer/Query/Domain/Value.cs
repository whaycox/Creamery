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
        static Value()
        {
        }

        public abstract TDataType DataType { get; }
    }
}
