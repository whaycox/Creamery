using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.DSIG.Implementation
{
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class DsigPersistor : PrimaryTablePersistor<DsigTable>
    {
        public override string Tag => nameof(DSIG);

        protected override DsigTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, DsigTable table)
        {
            throw new NotImplementedException();
        }
    }
}
