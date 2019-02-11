namespace Curds.Domain.Persistence
{
    public class MockNamedEntity : NamedEntity
    {
        public const int SampleID = 5;

        public static MockNamedEntity Sample => new MockNamedEntity() { ID = SampleID, Name = nameof(MockNamedEntity) };

        public override Entity Clone() => CloneInternal(new MockNamedEntity());
    }
}
