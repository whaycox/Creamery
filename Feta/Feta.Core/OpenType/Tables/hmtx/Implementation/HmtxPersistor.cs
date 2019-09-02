using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.hmtx.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class HmtxPersistor : PrimaryTablePersistor<HmtxTable>
    {
        public override string Tag => nameof(hmtx);

        protected override HmtxTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, HmtxTable table)
        {
            throw new NotImplementedException();
        }
    }
}
