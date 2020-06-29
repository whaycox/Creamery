using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.Tokens.Template
{
    using Implementation;

    public abstract class ObjectNameSqlQueryTokenTemplate : LiteralSqlQueryTokenTemplate
    {
        protected void VerifyLiteralWrapsNameInIdentifiers(ObjectNameSqlQueryToken testObject)
        {
            Assert.AreEqual($"[{testObject.Name}]", testObject.Literal);
        }
    }
}
