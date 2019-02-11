using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Persistence.Tests
{    
    [TestClass]
    public class Entity : EntityTemplate<Persistence.Entity>
    {
        protected override Persistence.Entity TestObject => MockEntity.Sample;
    }
}
