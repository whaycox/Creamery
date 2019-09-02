using System;

namespace Feta.OpenType.Tables.cvt.Implementation
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Implementation;

    public class CvtPersistor : PrimaryTablePersistor<CvtTable>
    {
        public override string Tag => "cvt ";

        protected override CvtTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, CvtTable table)
        {
            throw new NotImplementedException();
        }
    }
}
