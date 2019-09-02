using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.loca.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class LocaPersistor : PrimaryTablePersistor<LocaTable>
    {
        public override string Tag => nameof(loca);

        protected override LocaTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, LocaTable table)
        {
            throw new NotImplementedException();
        }
    }
}
