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

        byte? ReadByte(int index);
        short? ReadShort(int index);
        int? ReadInt(int index);
        long? ReadLong(int index);
        string ReadString(int index);
    }
}
