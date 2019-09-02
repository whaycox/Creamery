using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF.Implementation
{
    using ClassDefinition.Implementation;
    using ClassDefinition.Domain;
    using Domain;

    public class GlyphClassDefinitionPersistor : ClassDefinitionPersistor<GdefTable>
    {
        protected override void AttachSubtable(GdefTable parentTable, ClassDefinitionTable subTable) => parentTable.GlyphClassDefinition = subTable;
    }
}
