using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Persistence.Tests
{    
    [TestClass]
    public class NamedEntity : NamedEntityTemplate<Persistence.NamedEntity>
    {
        protected override Persistence.NamedEntity TestObject => MockNamedEntity.Sample;
    }
}
