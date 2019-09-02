using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.gasp.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class GaspPersistor : PrimaryTablePersistor<GaspTable>
    {
        public override string Tag => nameof(gasp);

        protected override GaspTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, GaspTable table)
        {
            throw new NotImplementedException();
        }
    }
}
