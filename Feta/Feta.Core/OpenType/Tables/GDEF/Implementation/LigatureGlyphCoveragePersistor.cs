using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF.Implementation
{
    using OpenType.Domain;
    using Coverage.Implementation;
    using Coverage.Domain;
    using Domain;

    public class LigatureGlyphCoveragePersistor : CoveragePersistor<GdefTable>
    {
        protected override void AttachSubtable(GdefTable parentTable, CoverageTable subTable) => parentTable.AddCoverage(subTable);
    }
}
