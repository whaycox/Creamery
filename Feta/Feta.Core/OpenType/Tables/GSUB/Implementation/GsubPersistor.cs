using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GSUB.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class GsubPersistor : PrimaryTablePersistor<GsubTable>
    {
        public override string Tag => nameof(GSUB);

        protected override GsubTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, GsubTable table)
        {
            throw new NotImplementedException();
        }
    }
}
