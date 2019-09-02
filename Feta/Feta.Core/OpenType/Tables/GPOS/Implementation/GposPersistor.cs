using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GPOS.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class GposPersistor : PrimaryTablePersistor<GposTable>
    {
        public override string Tag => nameof(GPOS);

        protected override GposTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, GposTable table)
        {
            throw new NotImplementedException();
        }
    }
}
