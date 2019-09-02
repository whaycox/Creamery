using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.maxp.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class MaxpPersistor : PrimaryTablePersistor<MaxpTable>
    {
        public override string Tag => nameof(maxp);

        protected override MaxpTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, MaxpTable table)
        {
            throw new NotImplementedException();
        }
    }
}
