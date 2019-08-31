using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Abstraction
{
    public delegate void TableParseDelegate(IFontReader reader);

    public interface IOffsetRegistry
    {
        void RegisterParser(uint offset, TableParseDelegate parser);
        TableParseDelegate RetrieveParser(uint offset);

        void RegisterStart(object key, uint offset);
        void RegisterEnd(object key, uint offset);

        uint RetrieveOffset(object key);
        uint RetrieveLength(object key);
    }
}
