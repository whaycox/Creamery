using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.prep.Implementation
{
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class PrepPersistor : PrimaryTablePersistor<PrepTable>
    {
        public override string Tag => nameof(prep);

        protected override PrepTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, PrepTable table)
        {
            throw new NotImplementedException();
        }
    }
}
