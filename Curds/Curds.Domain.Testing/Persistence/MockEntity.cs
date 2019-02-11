namespace Curds.Domain.Persistence
{
    public class MockEntity : Entity
    {
        public const int SampleID = 1;

        public static MockEntity Sample => new MockEntity() { ID = SampleID };

        public override Entity Clone() => CloneInternal(new MockEntity());
    }
}
