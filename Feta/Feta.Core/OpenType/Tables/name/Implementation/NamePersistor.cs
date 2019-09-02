using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.name.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class NamePersistor : PrimaryTablePersistor<NameTable>
    {
        public override string Tag => nameof(name);

        protected override NameTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, NameTable table)
        {
            throw new NotImplementedException();
        }
    }
}
