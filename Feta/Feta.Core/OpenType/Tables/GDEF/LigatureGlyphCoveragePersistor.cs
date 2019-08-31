using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF
{
    using OpenType.Domain;

    public class LigatureGlyphCoveragePersistor : Coverage.Persistor<Table>
    {
        protected override void AttachSubtable(Table parentTable, Coverage.Table subTable) => parentTable.AddCoverage(subTable);
    }
}
