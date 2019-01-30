namespace Curds.Domain.Persistence
{
    public class MockNamedEntity : NamedEntity
    {
        public override Entity Clone() => CloneInternal(new MockNamedEntity());
    }
}
