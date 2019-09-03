using System;
using System.Collections.Generic;
using System.Text;
using Feta.OpenType.Mock;
using Feta.OpenType.Tables.ClassDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.GDEF.Tests
{
    using ClassDefinition.Template;
    using ClassDefinition.Domain;
    using ClassDefinition.Implementation;
    using Domain;

    [TestClass]
    public class GlyphClassDefinitionPersistorTest : ClassDefinitionPersistorTemplate<GdefTable>
    {
        protected override ClassDefinitionPersistor<GdefTable> TestObject => throw new NotImplementedException();

        protected override void PrimeTableToRead(MockFontReader mockReader, ClassDefinitionTable table)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTablesAreEqual(ClassDefinitionTable expected, ClassDefinitionTable actual)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTableWasWritten(MockFontWriter mockWriter, ClassDefinitionTable table)
        {
            throw new NotImplementedException();
        }
    }
}
