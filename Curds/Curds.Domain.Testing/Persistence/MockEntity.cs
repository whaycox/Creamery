namespace Curds.Domain.Persistence
{
    public class MockEntity : Entity
    {
        public override Entity Clone() => CloneInternal(new MockEntity());
    }
}
