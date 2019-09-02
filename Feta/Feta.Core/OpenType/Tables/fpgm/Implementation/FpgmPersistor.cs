using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.fpgm.Implementation
{
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class FpgmPersistor : PrimaryTablePersistor<FpgmTable>
    {
        public override string Tag => nameof(fpgm);

        protected override FpgmTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, FpgmTable table)
        {
            throw new NotImplementedException();
        }
    }
}
