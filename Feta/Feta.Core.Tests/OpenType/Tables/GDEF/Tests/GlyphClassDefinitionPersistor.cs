using System;
using System.Collections.Generic;
using System.Text;
using Feta.OpenType.Mock;
using Feta.OpenType.Tables.ClassDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.GDEF.Tests
{
    [TestClass]
    public class GlyphClassDefinitionPersistor : ClassDefinition.Template.Persistor<Table>
    {
        protected override Persistor<Table> TestObject => throw new NotImplementedException();

        protected override void PrimeTableToRead(IFontReader mockReader, ClassDefinition.Table table)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTablesAreEqual(ClassDefinition.Table expected, ClassDefinition.Table actual)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTableWasWritten(IFontWriter mockWriter, ClassDefinition.Table table)
        {
            throw new NotImplementedException();
        }
    }
}
