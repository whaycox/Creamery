using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.OSTwo.Implementation
{
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class OsTwoPersistor : PrimaryTablePersistor<OsTwoTable>
    {
        public override string Tag => "OS/2";

        protected override OsTwoTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, OsTwoTable table)
        {
            throw new NotImplementedException();
        }
    }
}
