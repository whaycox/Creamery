using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.head.Implementation
{
    using OpenType.Abstraction;
    using Domain;
    using OpenType.Implementation;

    public class HeadPersistor : PrimaryTablePersistor<HeadTable>
    {
        public override string Tag => nameof(head);

        protected override HeadTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, HeadTable table)
        {
            throw new NotImplementedException();
        }
    }
}
