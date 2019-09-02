using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.hhea.Implementation
{
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class HheaPersistor : PrimaryTablePersistor<HheaTable>
    {
        public override string Tag => nameof(hhea);

        protected override HheaTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, HheaTable table)
        {
            throw new NotImplementedException();
        }
    }
}
