using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.OSTwo
{
    using Abstraction;
    using OpenType.Abstraction;
    using OpenType.Implementation;

    public class Persistor : ITablePersistor<Table>
    {
        public void Read(IFontReader reader)
        {
            throw new NotImplementedException();
        }

        public void Write(IFontWriter writer, Table table)
        {
            throw new NotImplementedException();
        }
    }
}
