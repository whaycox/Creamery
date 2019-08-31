using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF
{
    public class GlyphClassDefinitionPersistor : ClassDefinition.Persistor<Table>
    {
        protected override void AttachSubtable(Table parentTable, ClassDefinition.Table subTable) => parentTable.GlyphClassDefinition = subTable;
    }
}
