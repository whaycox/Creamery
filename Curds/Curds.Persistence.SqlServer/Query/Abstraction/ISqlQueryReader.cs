using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlQueryReader : IDisposable
    {
        Task<bool> Advance();

        string ReadString(string columnName);
        bool? ReadBool(string columnName);
        byte? ReadByte(string columnName);
        short? ReadShort(string columnName);
        int? ReadInt(string columnName);
        long? ReadLong(string columnName);
        DateTime? ReadDateTime(string columnName);
        DateTimeOffset? ReadDateTimeOffset(string columnName);
        decimal? ReadDecimal(string columnName);
        double? ReadDouble(string columnName);
    }
}
