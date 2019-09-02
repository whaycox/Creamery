using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.glyf.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class GlyfPersistor : PrimaryTablePersistor<GlyfTable>
    {
        public override string Tag => nameof(glyf);

        protected override GlyfTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, GlyfTable table)
        {
            throw new NotImplementedException();
        }
    }
}
