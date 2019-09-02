using System;

namespace Feta.OpenType.Tables.cmap.Implementation
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Implementation;

    public class CmapPersistor : PrimaryTablePersistor<CmapTable>
    {
        public override string Tag => nameof(cmap);

        protected override CmapTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, CmapTable table)
        {
            throw new NotImplementedException();
        }
    }
}
