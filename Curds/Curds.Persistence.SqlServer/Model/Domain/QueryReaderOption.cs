using System;

namespace Curds.Persistence.Model.Domain
{
    using Query.Abstraction;

    public class QueryReaderOption
    {
        public string ReadMethodName { get; set; }
        public Type ReadMethodType { get; set; }

        public static QueryReaderOption String => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadString), ReadMethodType = typeof(string) };
        public static QueryReaderOption Bool => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadBool), ReadMethodType = typeof(bool?) };
        public static QueryReaderOption Byte => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadByte), ReadMethodType = typeof(byte?) };
        public static QueryReaderOption Short => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadShort), ReadMethodType = typeof(short?) };
        public static QueryReaderOption Int => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadInt), ReadMethodType = typeof(int?) };
        public static QueryReaderOption Long => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadLong), ReadMethodType = typeof(long?) };
        public static QueryReaderOption DateTime => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadDateTime), ReadMethodType = typeof(DateTime?) };
        public static QueryReaderOption DateTimeOffset => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadDateTimeOffset), ReadMethodType = typeof(DateTimeOffset?) };
        public static QueryReaderOption Decimal => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadDecimal), ReadMethodType = typeof(decimal?) };
        public static QueryReaderOption Double => new QueryReaderOption { ReadMethodName = nameof(ISqlQueryReader.ReadDouble), ReadMethodType = typeof(double?) };
    }
}
