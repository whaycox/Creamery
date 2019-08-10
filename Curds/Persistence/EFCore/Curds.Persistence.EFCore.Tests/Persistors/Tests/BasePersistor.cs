using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Persistors.Tests
{
    using Mock;
    using EFCore.Mock;
    using Persistence.Mock;

    [TestClass]
    public class BasePersistor : Template.BasePersistor<INamedEntityPersistor, Context, INamedEntity>
    {
        protected override INamedEntityPersistor TestObject { get; } = new INamedEntityPersistor();
        
        protected override int ExpectedStartingCount => 3;
        protected override Context ExposedContext => TestObject.ExposedContext;
        protected override INamedEntity InsertEntity => new INamedEntity() { Name = nameof(InsertEntity) };

        protected override Context BuildContext() => new Context();
    }
}
